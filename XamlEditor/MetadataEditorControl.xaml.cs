// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows;
using System.Windows.Controls;

namespace XamlEditor
{
	public partial class MetadataEditorControl : UserControl
	{
		public Boolean IsDirty => _viewModel.IsDirty;

		private readonly ViewModel _viewModel  = new();
		public MetadataEditorControl()
		{
			InitializeComponent();
			DataContext = _viewModel;
		}

		private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			_viewModel.SelectedItem = e.NewValue;
		}

		public void LoadDocument(String path)
		{
			_viewModel.LoadDocument(path);
		}

		public void SaveDocument()
		{
			_viewModel.SaveDocument();
		}
	}
}
