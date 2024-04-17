﻿// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

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

		[JsonIgnore]
		protected override String ParentName => "catalog";

		[JsonIgnore]
		public override List<FieldNode> DefaultFields => _defaultFields;

		private readonly static List<FieldNode> _defaultFields = new List<FieldNode>()
		{
			new FieldNode() { Name = "Id", Type = FieldType.Id },
			new FieldNode() { Name = "Void", Type = FieldType.Boolean },
			new FieldNode() { Name = "Name", Length = 255 },
			new FieldNode() { Name = "Memo", Length = 255 }
		};
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
