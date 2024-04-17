// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace XamlEditor
{
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
		public ObservableCollection<CatalogNode> Catalogs { get; set; } = new ObservableCollection<CatalogNode>();
		public Boolean ShouldSerializeCatalogs() => Catalogs != null && Catalogs.Count > 0;

		[JsonProperty(Order = 12)]
		public ObservableCollection<DocumentNode> Documents { get; set; } = new ObservableCollection<DocumentNode>();
		public Boolean ShouldSerializeDocuments() => Documents != null && Documents.Count > 0;

		[JsonProperty(Order = 13)]
		public ObservableCollection<EndpointNode> Endpoints { get; set; } = new ObservableCollection<EndpointNode>();
		public Boolean ShouldSerializeEndpoints() => Endpoints != null && Endpoints.Count > 0;

		[JsonIgnore]
		public override IEnumerable<BaseNode> Children
		{
			get
			{
				yield return new CatalogsNode(Catalogs);
				yield return new DocumentsNode(Documents);
				yield return new EndpointsNode(Endpoints);
			}
		}

		public void AddCatalog()
		{
			var c = new CatalogNode(this) { Name = $"Catalog{Catalogs.Count + 1}" };
			c.ApplyDefaults();
			Catalogs.Add(c);
			c.IsSelected = true;
		}
		public void AddDocument()
		{
			var d = new DocumentNode(this) { Name = $"Document{Documents.Count + 1}" };
			d.ApplyDefaults();
			Documents.Add(d);
			d.IsSelected = true;
		}
		public void AddEndpoint()
		{
			var d = new EndpointNode(this) { Name = $"Endpoint{Endpoints.Count + 1}" };
			Endpoints.Add(d);
			d.IsSelected = true;
		}

		public void DeleteTable(TableNode table)
		{
			if (table is CatalogNode catalogNode)
				if (Catalogs.Contains(catalogNode))
					Catalogs.Remove(catalogNode);	
			else if (table is DocumentNode documentNode)
				if (Documents.Contains(documentNode))
					Documents.Remove(documentNode);
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
	}
}
