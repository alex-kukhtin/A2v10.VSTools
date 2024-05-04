using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace XamlEditor;

/// <summary>
/// Interaction logic for UserInterfaceDetailsPanel.xaml
/// </summary>
public partial class UserInterfaceDetailsPanel : UserControl
{
	public UserInterfaceDetailsPanel()
	{
		InitializeComponent();
		DataContextChanged += UserInterfaceDetailsPanel_DataContextChanged;
	}

	public IEnumerable<String> SourceFields => Node.RefFields.ToList();

	DetailsUiNode Node => DataContext as DetailsUiNode;
	private void UserInterfaceDetailsPanel_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
	{
		DefaultCheckBox.IsChecked = Node.Fields.Count == 0;
	}

	private void DefaultCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
	{
		if (!(sender is CheckBox checkBox))
			return;
		Node.SetDefault(checkBox.IsChecked == true);
	}
	private void AddField_Click(object sender, RoutedEventArgs e)
	{
	}

	private void DeleteField_Click(object sender, RoutedEventArgs e)
	{
		if (!(e.Source is Button btnObj) || !(btnObj.CommandParameter is UiFieldNode fn))
			return;
		Node.Fields.Remove(fn);
	}
	private void FieldUp_Click(object sender, RoutedEventArgs e)
	{
		if (e.Source is not Button btnObj || btnObj.CommandParameter is not UiFieldNode fn)
			return;
		int pos = Node.Fields.IndexOf(fn);
		if (pos < 1)
			return;
		Node.Fields.Move(pos, pos - 1);
	}

	private void FieldDown_Click(object sender, RoutedEventArgs e)
	{
		if (e.Source is not Button btnObj || btnObj.CommandParameter is not UiFieldNode fn)
			return;
		int pos = Node.Fields.IndexOf(fn);
		if (pos >= Node.Fields.Count - 1)
			return;
		Node.Fields.Move(pos, pos + 1);
	}
}
