// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class ViewModel : INotifyPropertyChanged
	{
		private AppNode _appNode = new AppNode() { Name = "Application" };
		private String _path;
		public ObservableCollection<AppNode> Root { get; } = new ObservableCollection<AppNode>();

		public AppNode AppNode => _appNode;
		public Boolean IsDirty => _appNode.IsDirty;
		public ViewModel()
		{
			Root.Add(_appNode);
			_appNode.IsSelected = true;
		}

		public void LoadDocument(String path)
		{
			_path = path;
			var content = System.IO.File.ReadAllText(_path);
			Root.Remove(_appNode);
			_appNode = JsonConvert.DeserializeObject<AppNode>(content, JsonHelpers.DefaultSettings);
			Root.Add(_appNode);
			_appNode.IsSelected = true;
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
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] String prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		private AppMenuCommand _addDetailsCommand;
		private AppMenuCommand _addCommand;
		public AppMenuCommand AddCommand 
		{
			get
			{
				_addCommand = _addCommand ?? new AddCommand(this);
				return _addCommand;
			}
		}
		public AppMenuCommand DeleteCommand => new DeleteCommand(this);
		public AppMenuCommand AddDetailsCommand 
		{ 
			get {
				_addDetailsCommand = _addDetailsCommand ?? new AddDetailsCommand(this);
				return _addDetailsCommand;
			} 
		}
	}
}
