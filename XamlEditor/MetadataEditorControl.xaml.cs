// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows;
using System.Windows.Controls;

namespace XamlEditor;

public partial class MetadataEditorControl : UserControl
{
	public Boolean IsDirty => _viewModel.InitComplete && _viewModel.IsDirty;

	private readonly ViewModel _viewModel  = new();
	private readonly String _solutionName;

	public MetadataEditorControl()
	{
		_solutionName = "TEST";
		InitializeComponent();
		DataContext = _viewModel;
	}

	public MetadataEditorControl(String solutionName)
	{
		_solutionName = solutionName;
		InitializeComponent();
		DataContext = _viewModel;
	}

	public void HideSave()
	{
		_viewModel.ShowSave = false;
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

	private void DeployDatabase_Click(object sender, RoutedEventArgs e)
	{
		var deploy = new DeployDatabase(_viewModel, _solutionName)
		{
			Owner = Application.Current.MainWindow
		};
		deploy.ShowDialog();
	}

	private void Save_Click(object sender, RoutedEventArgs e)
	{
		_viewModel.SaveDocument();
	}
}
