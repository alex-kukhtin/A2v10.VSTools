using System;
using System.Windows;

namespace XamlEditor;

/// <summary>
/// Interaction logic for DeployDatabase.xaml
/// </summary>
public partial class DeployDatabase : Window
{
	private readonly DeployViewModel _viewModel;
	private readonly ViewModel _root;
	public DeployDatabase(ViewModel vm, String solutionName)
	{
		_root = vm;
		InitializeComponent();
		_viewModel = new DeployViewModel()
		{
			Server = "localhost",
			Database = solutionName
		};
		DataContext = _viewModel;
	}

	private void CloseBtn_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
		Close();
	}

	private async void DeployBtn_Click(object sender, RoutedEventArgs e)
	{
		_viewModel.IsProgress = true;

		await SqlDeploy.DeployDatabase(_root, _viewModel.Server, _viewModel.Database, v => _viewModel.Progress = v);

		_viewModel.IsProgress = false;
		DialogResult = true;
		Close();
	}
}
