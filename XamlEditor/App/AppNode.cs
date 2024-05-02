﻿// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace XamlEditor;

public class AppNode : BaseNode
{
	[JsonIgnore]
	internal Boolean IsDirty { get; set; }
	public AppNode()
	{
		Catalogs.CollectionChanged += CollectionChanged;
		Documents.CollectionChanged += CollectionChanged;
		Endpoints.CollectionChanged += CollectionChanged;
	}

	[JsonIgnore]
	protected override String ImageName => "AppNode";

	[JsonProperty(Order = 11)]
	public ObservableCollection<CatalogNode> Catalogs { get; set; } = [];
	public Boolean ShouldSerializeCatalogs() => Catalogs != null && Catalogs.Count > 0;

	[JsonProperty(Order = 12)]
	public ObservableCollection<DocumentNode> Documents { get; set; } = [];
	public Boolean ShouldSerializeDocuments() => Documents != null && Documents.Count > 0;

	[JsonProperty(Order = 13)]
	public ObservableCollection<JournalNode> Journals { get; set; } = [];
	public Boolean ShouldSerializeJournal() => Journals != null && Journals.Count > 0;

	[JsonProperty(Order = 14)]
	public ObservableCollection<EndpointNode> Endpoints { get; set; } = [];
	public Boolean ShouldSerializeEndpoints() => Endpoints != null && Endpoints.Count > 0;

	[JsonIgnore]
	public override IEnumerable<BaseNode> Children
	{
		get
		{
			yield return new CatalogsNode(Catalogs);
			yield return new DocumentsNode(Documents);
			yield return new JournalsNode(Journals);
			yield return new EndpointsNode(Endpoints);
			yield return new MenuNode() { Name = "Menu" };
		}
	}

	public void AddCatalog()
	{
		var c = new CatalogNode(this) { Name = $"Catalog{Catalogs.Count + 1}" };
		c.ApplyDefaults();
		Catalogs.Add(c);
		c.IsSelected = true;
		c.OnInit();
	}
	public void AddDocument()
	{
		var d = new DocumentNode(this) { Name = $"Document{Documents.Count + 1}" };
		d.ApplyDefaults();
		Documents.Add(d);
		d.IsSelected = true;
		d.OnInit();
	}
	public void AddJournal()
	{
		var j = new JournalNode(this) { Name = $"Journal{Journals.Count + 1}" };
		j.ApplyDefaults();
		Journals.Add(j);
		j.IsSelected = true;
		j.OnInit();
	}
	public void AddEndpoint()
	{
		var d = new EndpointNode(this) { Name = $"Endpoint{Endpoints.Count + 1}" };
		Endpoints.Add(d);
		d.IsSelected = true;
		d.OnInit(this);
	}

	public void DeleteTable(TableNode table)
	{
		if (table is CatalogNode catalogNode)
			if (Catalogs.Contains(catalogNode))
				Catalogs.Remove(catalogNode);	
		else if (table is DocumentNode documentNode)
			if (Documents.Contains(documentNode))
				Documents.Remove(documentNode);
		else if (table is JournalNode journalNode)
			if (Journals.Contains(journalNode))
				Journals.Remove(journalNode);
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

	internal override void OnInit()
	{
		base.OnInit();
		foreach (var t in Endpoints)
			t.OnInit(this);
	}
}
