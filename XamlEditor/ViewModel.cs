// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace XamlEditor
{
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

		public Boolean InitComplete { get; set; }
		public ViewModel()
		{
			Root.Add(_appNode);
			_appNode.IsSelected = true;
			Root.Add(new SqlScriptNode(_appNode));
		}

		public void LoadDocument(String path)
		{
			_path = path;
			var content = System.IO.File.ReadAllText(_path);
			Root.Clear();
			_appNode = JsonConvert.DeserializeObject<AppNode>(content, JsonHelpers.DefaultSettings);
			Root.Add(_appNode);
			Root.Add(new SqlScriptNode(_appNode));
			_appNode.OnInit(_appNode);
			_appNode.IsSelected = true;
			_appNode.IsDirty = false;
			InitComplete = true;
		}
		public void SaveDocument()
		{
			_appNode.IsDirty = false;
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
	}
}
