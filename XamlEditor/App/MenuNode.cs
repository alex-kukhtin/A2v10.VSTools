// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

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
		{
			item.OnInit(root);
			item.SetLevel(0);
		}
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

	public ObservableCollection<MenuItemNode> FindCollection()
	{
		var sel = SelectedItem;
		if (Items.IndexOf(sel) != -1)
			return Items;
		else
			foreach (var item in Items)
				return item.FindCollection(sel);
		return null;
	}
}

public record MenuSqlNode
{
	public Guid Id;
	public Guid Parent;
	public String Name;
	public String Url;
	public Int32 Order;
	public String Icon;

	public String MenuClassName()
	{
		return Name == "<Aligner>" ? "N'grow'" : "null";
	}

	public String MenuName()
	{
		return Name == "<Aligner>" ? null : Name;
	}
}
public class MenuItemNode : BaseNode
{
	public MenuItemNode()
	{
		Items.CollectionChanged += Items_CollectionChanged;
	}

	private void Items_CollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
	{
		if (_root != null)
			_root.IsDirty = true;
	}

	[JsonProperty(Order = 3)]
	public ObservableCollection<MenuItemNode> Items { get; set; } = [];
	public Boolean ShouldSerializeItems() => Items.Count > 0;

	Guid _id = Guid.NewGuid();
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
	internal ObservableCollection<MenuItemNode> FindCollection(MenuItemNode sel)
	{
		if (Items.IndexOf(sel) != -1)
			return Items;
		else
			foreach (var item in Items)
			{
				var x = item.FindCollection(sel);
				if (x != null)
					return x;
			}
		return null;
	}

	internal void SetLevel(Int32 level)
	{
		Level = level;	
		foreach (var item in Items)
			item.SetLevel(level + 1);
	}

	internal IEnumerable<MenuSqlNode> PlainElements(Guid parent, Int32 order = 0)
	{
		yield return new MenuSqlNode() { Id = this.Id, Parent = parent, Order = order, Name = this.Name, Icon = this.Icon, Url = this.Url };
		Int32 _order = 0;
		foreach (var m in Items)
		{
			foreach (var pe in m.PlainElements(this.Id, _order += 10))
				yield return pe;
		}			
	}

	internal override void OnInit(AppNode root)
	{
		_root = root;
		foreach (var itm in Items)
			itm.OnInit(root);	
		base.OnInit(root);
	}
}
