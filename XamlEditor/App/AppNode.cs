// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace XamlEditor;

public class AppNode : BaseNode
{
	public AppNode()
	{
		Catalogs.CollectionChanged += CollectionChanged;
		Documents.CollectionChanged += CollectionChanged;
		Endpoints.CollectionChanged += CollectionChanged;
		Menu.CollectionChanged += CollectionChanged;
	}
	
	private String _title;

	[JsonProperty(Order = 1)]
	public String Title { get => _title; set { _title = value; OnPropertyChanged(); } }

	private Guid _id = Guid.NewGuid();	

	[JsonProperty(Order = -1)]
	public Guid Id { get => _id; set => _id = value; }

	[JsonIgnore]
	internal Boolean IsDirty { get; set; }

	[JsonIgnore] 
	public Boolean IsEmpty 
	{ 
		get => Catalogs.Count == 0 && Documents.Count == 0 && Journals.Count == 0 && Endpoints.Count == 0
				&& Menu.Count == 0;
	}

	[JsonIgnore]
	protected override String ImageName => "AppNode";

	[JsonProperty(Order = 11)]
	public ObservableCollection<CatalogNode> Catalogs { get; set; } = [];
	public Boolean ShouldSerializeCatalogs() => Catalogs.Count > 0;

	[JsonProperty(Order = 12)]
	public ObservableCollection<DocumentNode> Documents { get; set; } = [];
	public Boolean ShouldSerializeDocuments() => Documents.Count > 0;

	[JsonProperty(Order = 13)]
	public ObservableCollection<JournalNode> Journals { get; set; } = [];
	public Boolean ShouldSerializeJournals() => Journals.Count > 0;

	[JsonProperty(Order = 14)]
	public ObservableCollection<EndpointNode> Endpoints { get; set; } = [];
	public Boolean ShouldSerializeEndpoints() => Endpoints.Count > 0;

	[JsonProperty(Order = 15)]
	public ObservableCollection<MenuItemNode> Menu { get; set; } = [];
	public Boolean ShouldSerializeMenu() => Menu.Count > 0;

	private MenuNode _menuNode;
	[JsonIgnore]
	public override IEnumerable<BaseNode> Children
	{
		get
		{
			yield return new CatalogsNode(Catalogs);
			yield return new DocumentsNode(Documents);
			yield return new JournalsNode(Journals);
			yield return new EndpointsNode(Endpoints);
			_menuNode ??= new MenuNode(this, Menu) { Name = "Menu" };
			yield return _menuNode;
		}
	}

	public void AddCatalog()
	{
		var c = new CatalogNode(this) { Name = $"Catalog{Catalogs.Count + 1}" };
		c.ApplyDefaults();
		Catalogs.Add(c);
		c.IsSelected = true;
		c.OnInit(this);
	}
	public void AddDocument()
	{
		var d = new DocumentNode(this) { Name = $"Document{Documents.Count + 1}" };
		d.ApplyDefaults();
		Documents.Add(d);
		d.IsSelected = true;
		d.OnInit(this);
	}
	public void AddJournal()
	{
		var j = new JournalNode(this) { Name = $"Journal{Journals.Count + 1}" };
		j.ApplyDefaults();
		Journals.Add(j);
		j.IsSelected = true;
		j.OnInit(this);
	}
	public void AddEndpoint()
	{
		var d = new EndpointNode(this) { Name = $"Endpoint{Endpoints.Count + 1}" };
		Endpoints.Add(d);
		d.IsSelected = true;
		d.OnInit(this);
	}

	public void DeleteElement(BaseNode table)
	{
		if (table is CatalogNode catalogNode)
		{
			if (Catalogs.Contains(catalogNode))
				Catalogs.Remove(catalogNode);
		}
		else if (table is DocumentNode documentNode)
		{
			if (Documents.Contains(documentNode))
				Documents.Remove(documentNode);
		}
		else if (table is JournalNode journalNode)
		{
			if (Journals.Contains(journalNode))
				Journals.Remove(journalNode);
		}
		else if (table is EndpointNode endpointNode)
		{
			if (Endpoints.Contains(endpointNode))
				Endpoints.Remove(endpointNode);
		}
		else if (table is DetailsNode detailsNode)
		{
			foreach (var c in Catalogs)
			{
				if (c.Details.Contains(detailsNode))
				{
					c.Details.Remove(detailsNode);
					return;
				}
			}
			foreach (var d in Documents)
			{
				if (d.Details.Contains(detailsNode))
				{
					d.Details.Remove(detailsNode);
					return;
				}
			}
		}
	}

	public TableNode FindNode(String table)
	{
		if (String.IsNullOrEmpty(table))
			return null;
		var c = Catalogs.FirstOrDefault(x => $"Catalog.{x.Name}" == table);
		if (c != null) 
			return c;
		var d = Documents.First(x => $"Document.{x.Name}" == table);
		if (d != null) 
			return d;
		return null;
	}

	private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
	{
		IsDirty = true;
	}


	[JsonIgnore]
	public String JsonValue { 
		get
		{
			var x = JsonConvert.SerializeObject(this, JsonHelpers.DefaultSettings);
			return x;
		} 
	}

	internal override void OnInit(AppNode root)
	{
		base.OnInit(this);
		foreach (var item in Catalogs)
			item.OnInit(this);
		foreach (var item in Documents)
			item.OnInit(this);
		foreach (var item in Journals)
			item.OnInit(this);
		foreach (var t in Endpoints)
			t.OnInit(this);
		_menuNode ??= new MenuNode(this, Menu) { Name = "Menu" };
		_menuNode?.OnInit(this);
	}
}
