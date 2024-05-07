using System.Windows;

namespace XamlEditor;

/// <summary>
/// Interaction logic for DeployDatabase.xaml
/// </summary>
public partial class DeployDatabase : Window
{
	DeployViewModel _viewModel;
	AppNode _root;
	public DeployDatabase(AppNode root)
	{
		_root = root;
		InitializeComponent();
		_viewModel = new DeployViewModel()
		{
			Server = "localhost",
			Database = "appbuilder_demo"
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
