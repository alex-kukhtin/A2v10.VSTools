// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace XamlEditor;

public partial class AppPanel : UserControl
{
	private readonly ViewModel _viewModel;
	public AppPanel(AppNode app, ViewModel viewModel)
	{
		_viewModel = viewModel;
		InitializeComponent();
		DataContext = app;
	}

	public ICommand CreateSampleCommand => _viewModel.CreateSampleCommand;
}
