// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace XamlEditor;

public partial class MenuPanel : UserControl
{
	private readonly MenuNode _menuNode;
	public MenuPanel(MenuNode node, ViewModel viewModel)
	{
		_menuNode = node;

		InitializeComponent();

		DataContext = node;
	}

	private void TreeView_SelectedItemChanged(Object sender, RoutedPropertyChangedEventArgs<Object> e)
	{
		_menuNode.SelectedItem = (MenuItemNode) e.NewValue;
		_menuNode.OnPropertyChanged(nameof(MenuNode.HasSelected));
	}

	private void AddMenu_Click(Object sender, RoutedEventArgs e)
	{
		var item = new MenuItemNode()
		{
			Id = Guid.NewGuid(),
			Name = $"Menu{_menuNode.Items.Count + 1}"
		};
		_menuNode.Items.Add(item);
		SelectItem(item);	
	}

	private void AddItem_Click(Object sender, RoutedEventArgs e)
	{
		if (_menuNode.SelectedItem == null) 
			return;
		if (_menuNode.SelectedItem.Level >= 2)
			return;
		var item = new MenuItemNode() 
		{
			Id = Guid.NewGuid(),
			Name = $"Menu{_menuNode.SelectedItem.Items.Count + 1}",
			Level = _menuNode.SelectedItem.Level + 1
		};
		_menuNode.SelectedItem.Items.Add(item);
		SelectItem(item);
	}

	void SelectItem(MenuItemNode item)
	{
		item.IsSelected = true;
		_menuNode.OnPropertyChanged(nameof(MenuNode.SelectedItem));
		_menuNode.OnPropertyChanged(nameof(MenuNode.HasSelected));
	}

	private void DeleteItem_Click(object sender, RoutedEventArgs e)
	{
		if (_menuNode.SelectedItem == null)
			return;
		_menuNode.RemoveSelected();
	}

	private void MoveUp_Click(object sender, RoutedEventArgs e)
	{
		if (_menuNode.SelectedItem == null)
			return;
		var sel = _menuNode.SelectedItem;
		var coll = _menuNode.FindCollection();
		if (coll == null)
			return;
		int pos = coll.IndexOf(sel);
		if (pos < 1)
			return;
		coll.Move(pos, pos - 1);

	}

	private void MoveDown_Click(object sender, RoutedEventArgs e)
	{
		if (_menuNode.SelectedItem == null)
			return;
		var sel = _menuNode.SelectedItem;
		var coll = _menuNode.FindCollection();
		if (coll == null)
			return;
		int pos = coll.IndexOf(sel);
		if (pos >= coll.Count - 1)
			return;
		coll.Move(pos, pos + 1);

	}
}
