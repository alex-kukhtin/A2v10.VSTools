// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Windows;
using Newtonsoft.Json;

namespace XamlEditor;

public class ViewModel : INotifyPropertyChanged
{
	private AppNode _appNode = new() { Name = "Application" };
	private String _path;
	public ObservableCollection<BaseNode> Root { get; } = [];

	public AppNode RootNode => _appNode;
	public IEnumerable<String> RefTables =>
		_appNode.Catalogs.Select(c => $"Catalog.{c.Name}")
		.Union(_appNode.Documents.Select(d => $"Document.{d.Name}"));

	public AppNode AppNode => _appNode;
	public Boolean IsDirty => _appNode.IsDirty;

	public Boolean ShowSave { get; set; }
	public Boolean InitComplete { get; set; }
	public ViewModel()
	{
		Root.Add(_appNode);
		_appNode.IsSelected = true;
		Root.Add(new JsonScriptNode(_appNode));
		Root.Add(new SqlScriptNode(_appNode));
		ShowSave = true;
	}

	public void LoadDocument(String path)
	{
		_path = path;
		var content = File.ReadAllText(_path);
		_appNode = JsonConvert.DeserializeObject<AppNode>(content, JsonHelpers.DefaultSettings);
		OnCreateAppNode();
	}

	private void OnCreateAppNode()
	{ 
		Root.Clear();
		Root.Add(_appNode);
		Root.Add(new JsonScriptNode(_appNode));
		Root.Add(new SqlScriptNode(_appNode));
		_appNode.OnInit(_appNode);
		_appNode.IsSelected = true;
		_appNode.IsDirty = false;
		InitComplete = true;
	}
	public void SaveDocument()
	{
		_appNode.IsDirty = false;
		var json = _appNode.JsonValue;
		File.WriteAllText(_path, json);

		var dir = Path.GetDirectoryName(_path);
		var sb = new SqlTextBuilder(_appNode);

		var tables = sb.BuildTables();
		File.WriteAllText(Path.Combine(dir, "_sql/_tables.sql"), tables);

		var tableTypes = sb.BuildTableTypes();
		File.WriteAllText(Path.Combine(dir, "_sql/_tabletypes.sql"), tableTypes);

		var schemas = sb.BuildSchemas();
		File.WriteAllText(Path.Combine(dir, "_sql/_schemas.sql"), schemas);

		var ui = sb.BuildUI();
		File.WriteAllText(Path.Combine(dir, "_sql/_ui.sql"), ui);
	}

	private Object _selectedItem;
	public Object SelectedItem
	{
		get
		{
			return _selectedItem;
		}
		set
		{
			_selectedItem = value;
			OnPropertyChanged();
			_addDetailsCommand?.OnCanExecuteChanged();
			_addCommand?.OnCanExecuteChanged();
			_deleteCommand?.OnCanExecuteChanged();
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] String prop = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
	}

	private AppMenuCommand _addDetailsCommand;
	private AppMenuCommand _addCommand;
	private DeleteCommand _deleteCommand;
	public AppMenuCommand AddCommand 
	{
		get
		{
			_addCommand ??= new AddCommand(this);
			return _addCommand;
		}
	}
	public AppMenuCommand DeleteCommand
	{
		get
		{
			_deleteCommand ??= new DeleteCommand(this);	
			return _deleteCommand;
		}
	} 
	public AppMenuCommand AddDetailsCommand 
	{ 
		get {
			_addDetailsCommand ??= new AddDetailsCommand(this);
			return _addDetailsCommand;
		} 
	}

	public CreateSampleCommand CreateSampleCommand => new CreateSampleCommand(this);	

	public void CreateSampleApplication(String appName)
	{
		var uri = new Uri($"/XamlEditor;Component/AppSamples/{appName}.json", UriKind.Relative);
		var streamInfo = Application.GetResourceStream(uri);
		using var sr = new StreamReader(streamInfo.Stream);
		var text = sr.ReadToEnd();
		streamInfo.Stream.Close();
		_appNode = JsonConvert.DeserializeObject<AppNode>(text, JsonHelpers.DefaultSettings);
		_appNode.Id = Guid.NewGuid();
		OnCreateAppNode();
		_appNode.OnPropertyChanged(String.Empty);
	}
}
