// Copyright © 2022-2024 Oleksandr Kukhtin. All rights reserved.


using System.Windows;
using System.Windows.Controls;

namespace XamlEditor
{
	public partial class UserInterfaceIndexPanel : UserControl
	{
		private readonly AppNode _root;
		private readonly EndpointNode _endpoint;
		private readonly BaseUiNode _baseNode;
		public UserInterfaceIndexPanel(BaseUiNode node, EndpointNode endpoint)
		{
			_root = endpoint._root;
			_endpoint = endpoint;
			_baseNode = node;
			InitializeComponent();
			DataContext = _baseNode;
			DefaultCheckBox.IsChecked = node.Fields.Count == 0;
		}
		private void AddField_Click(object sender, RoutedEventArgs e)
		{
		}

		private void DeleteField_Click(object sender, RoutedEventArgs e)
		{
			if (!(e.Source is Button btnObj) || !(btnObj.CommandParameter is UiFieldNode fn))
				return;
			_baseNode.Fields.Remove(fn);
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			if (!(sender is CheckBox checkBox))
				return;
			_baseNode.SetDefault(checkBox.IsChecked == true ? null : _root.FindNode(_endpoint.Table));
		}
	}
}
