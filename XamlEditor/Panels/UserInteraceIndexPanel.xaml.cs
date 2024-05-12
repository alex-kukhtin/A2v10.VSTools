// Copyright © 2022-2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace XamlEditor;

public partial class UserInterfaceIndexPanel : UserControl
{
	private readonly AppNode _root;
	private readonly EndpointNode _endpoint;
	private readonly IndexUiNode _baseNode;
	public UserInterfaceIndexPanel(IndexUiNode node, EndpointNode endpoint)
	{
		_root = endpoint._root;
		_endpoint = endpoint;
		_baseNode = node;
		InitializeComponent();
		DataContext = _baseNode;
		DefaultCheckBox.IsChecked = node.Fields.Count == 0;
	}
	public IEnumerable<String> SourceFields => _baseNode.RefFields.ToList();

	private void AddField_Click(object sender, RoutedEventArgs e)
	{
		var field = new UiFieldNode();
		field.SetParent(_endpoint);
		_baseNode.Fields.Add(field);
	}

	private void DeleteField_Click(object sender, RoutedEventArgs e)
	{
		if (e.Source is not Button btnObj || btnObj.CommandParameter is not UiFieldNode fn)
			return;
		_baseNode.Fields.Remove(fn);
	}

	private void CheckBox_Checked(object sender, RoutedEventArgs e)
	{
		if (sender is not CheckBox checkBox)
			return;
		_baseNode.SetDefault(checkBox.IsChecked ?? false);
	}
	private void FieldUp_Click(object sender, RoutedEventArgs e)
	{
		if (e.Source is not Button btnObj || btnObj.CommandParameter is not UiFieldNode fn)
			return;
		int pos = _baseNode.Fields.IndexOf(fn);
		if (pos < 1)
			return;
		_baseNode.Fields.Move(pos, pos - 1);
	}

	private void FieldDown_Click(object sender, RoutedEventArgs e)
	{
		if (e.Source is not Button btnObj || btnObj.CommandParameter is not UiFieldNode fn)
			return;
		int pos = _baseNode.Fields.IndexOf(fn);
		if (pos >= _baseNode.Fields.Count - 1)
			return;
		_baseNode.Fields.Move(pos, pos + 1);
	}
}
