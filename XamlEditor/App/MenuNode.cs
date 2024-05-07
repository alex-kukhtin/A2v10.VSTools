// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamlEditor;

public class MenuNode : BaseNode
{
	public MenuNode(AppNode root, ObservableCollection<MenuItemNode> items)
	{
		_root = root;	
		Items = items;
	}
	protected override String ImageName => "MainMenuControl";
	public ObservableCollection<MenuItemNode> Items { get; private set; }

	MenuItemNode _selectedItem;
	public MenuItemNode SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); } }

	internal override void OnInit(AppNode root)
	{
		base.OnInit(root);
		foreach (var item in Items)
			item.SetLevel(0);
	}

	public IEnumerable<String> AllEnpoints()
	{
		foreach (var catalog in _root.Catalogs)
			yield return catalog.Endpoint;
		foreach (var document in _root.Documents)
			yield return document.Endpoint;
		foreach (var enpoint in _root.Endpoints)
			yield return enpoint.Name;
	}
	public IEnumerable<String> AvailableEndpoints
	{
		get => AllEnpoints().Distinct();
	}

	public Boolean HasSelected => SelectedItem != null;

	public void RemoveSelected()
	{
		var sel = SelectedItem;
		if (Items.IndexOf(sel) != -1)
			Items.Remove(sel);
		else
			foreach (var item in Items)
				item.RemoveItem(sel);	
	}
}

public class MenuItemNode : BaseNode
{
	[JsonProperty(Order = 3)]
	public ObservableCollection<MenuItemNode> Items { get; set; } = [];
	public Boolean ShouldSerializeItems() => Items.Count > 0;

	Guid _id;
	[JsonProperty(Order = -10)]
	public Guid Id { get => _id; set { _id = value; OnPropertyChanged(); } }

	String _url;
	[JsonProperty(Order = 1)]
	public String Url { get => _url; set { _url = value; OnPropertyChanged(); } }

	String _icon;
	[JsonProperty(Order = 2)]
	public String Icon { get => _icon; set { _icon = value; OnPropertyChanged(); } }

	protected override String ImageName => "MainMenuControl";

	[JsonIgnore]
	internal Int32 Level { get; set; }

	internal void RemoveItem(MenuItemNode sel)
	{
		if (Items.IndexOf(sel) != -1) 
			Items.Remove(sel);
		else
			foreach (var item in Items)
				item.RemoveItem(sel);
	}

	internal void SetLevel(Int32 level)
	{
		Level = level;	
		foreach (var item in Items)
			item.SetLevel(level + 1);
	}
}
