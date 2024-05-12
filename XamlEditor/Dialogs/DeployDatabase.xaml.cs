using System;
using System.Windows;

namespace XamlEditor;

/// <summary>
/// Interaction logic for DeployDatabase.xaml
/// </summary>
public partial class DeployDatabase : Window
{
	private readonly DeployViewModel _viewModel;
	private readonly AppNode _root;
	public DeployDatabase(AppNode root, String solutionName)
	{
		_root = root;
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

	private void DeployBtn_Click(object sender, RoutedEventArgs e)
	{
		MessageBox.Show("Deploy database at: " + _viewModel.ConnectionString,
			"Deploy", MessageBoxButton.OK, MessageBoxImage.Information);
	}
}
