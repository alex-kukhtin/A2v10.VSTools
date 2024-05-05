// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows;
using System.Windows.Controls;

namespace XamlEditor;

public partial class MenuPanel : UserControl
{
	private readonly MenuNode _menuNode;
	public MenuPanel(MenuNode node, ViewModel viewModel)
	{
		_menuNode = node;

		InitializeComponent();

		DataContext = node;

		// TODO: Kill me
		if (node.Items.Count > 0)
			return;
		node.Items.Add(new MenuItemNode()
		{
			Id = Guid.NewGuid(),
			Name = "Menu1",
			Items = [
				new MenuItemNode() {
					Id = Guid.NewGuid(),
					Name = "SubMenu1.1"
				}
			]
		});

		node.Items.Add(new MenuItemNode()
		{
			Id = Guid.NewGuid(),
			Name = "Menu2",
			Items = [
				new MenuItemNode() {
					Id = Guid.NewGuid(),
					Name = "SubMenu2.1"
				},
				new MenuItemNode() {
					Id = Guid.NewGuid(),
					Name = "SubMenu2.2"
				}
			]
		});
	}

	private void TreeView_SelectedItemChanged(Object sender, RoutedPropertyChangedEventArgs<Object> e)
	{
		_menuNode.SelectedItem = (MenuItemNode) e.NewValue;
	}

	private void AddMenu_Click(Object sender, RoutedEventArgs e)
	{
		_menuNode.Items.Add(new MenuItemNode() {
			Id = Guid.NewGuid(),
			Name =  $"Menu{_menuNode.Items.Count + 1}"
		});
	}

	private void AddItem_Click(Object sender, RoutedEventArgs e)
	{
		if (_menuNode.SelectedItem == null) 
			return;
		var item = new MenuItemNode() 
		{
			Id = Guid.NewGuid(),
			Name = $"Menu{_menuNode.SelectedItem.Items.Count + 1}" 
		};
		_menuNode.SelectedItem.Items.Add(item);
		item.IsSelected = true;
		_menuNode.OnPropertyChanged(nameof(MenuNode.SelectedItem));
	}
}
