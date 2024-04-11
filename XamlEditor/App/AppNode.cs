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
		}

		[JsonIgnore]
		protected override String ImageName => "AppNode";

		[JsonProperty(Order = 11)]
		public ObservableCollection<CatalogNode> Catalogs { get; set; } = new ObservableCollection<CatalogNode>();
		public Boolean ShouldSerializeCatalogs() => Catalogs != null && Catalogs.Count > 0;

		[JsonProperty(Order = 11)]
		public ObservableCollection<DocumentNode> Documents { get; set; } = new ObservableCollection<DocumentNode>();
		public Boolean ShouldSerializeDocuments() => Documents != null && Documents.Count > 0;

		[JsonIgnore]
		public override IEnumerable<BaseNode> Children
		{
			get
			{
				yield return new CatalogsNode(Catalogs);
				yield return new DocumentsNode(Documents);
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

		private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			IsDirty = true;
		}

		private readonly static JsonSerializerSettings DefaultSettings = new JsonSerializerSettings()
		{
			Formatting = Formatting.Indented,
			NullValueHandling = NullValueHandling.Ignore,	
			DefaultValueHandling = DefaultValueHandling.Ignore,
			ContractResolver = new DefaultContractResolver()
			{
				NamingStrategy = new CamelCaseNamingStrategy()
			}
		};

		[JsonIgnore]
		public String JsonValue { 
			get
			{
				var x = JsonConvert.SerializeObject(this, DefaultSettings);
				return x;
			} 
		}
	}
}
