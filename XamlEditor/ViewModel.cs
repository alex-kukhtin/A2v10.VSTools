
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace XamlEditor
{
	public class ViewModel : INotifyPropertyChanged
	{
		private readonly AppNode _appNode = new AppNode() { Name = "Application" };
		private String _path;
		public ObservableCollection<AppNode> Root { get; } = new ObservableCollection<AppNode>();

		public Boolean IsDirty => _appNode.IsDirty;	
		public  ViewModel()
		{
			Root.Add(_appNode);
			_appNode.IsSelected = true;
		}

		public void LoadDocument(String path)
		{
			_path = path;
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
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] String prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public ICommand AddCommand => new AddCommand(this);
	}
}
