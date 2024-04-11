// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class CatalogNode : TableNode
	{
		public CatalogNode(AppNode root) 
		{
			_root = root;
		}
		[JsonIgnore]
		protected override String ImageName => "Catalog";

		protected override String ParentName => "catalog";
	}

	public class CatalogsNode : BaseNode
	{
		private readonly ObservableCollection<CatalogNode> _catalogs;
		public CatalogsNode(ObservableCollection<CatalogNode> catalogs)
		{
			Name = "Catalogs";
			_catalogs = catalogs;
		}
		public override IEnumerable<BaseNode> Children => _catalogs;

	}
}
