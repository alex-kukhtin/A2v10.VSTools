// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class DetailsNode : TableNode
	{
		private readonly TableNode _parentTable;
		public DetailsNode(AppNode root, TableNode parent) 
		{
			_root = root;
			_parentTable = parent;
		}
		[JsonIgnore]
		protected override String ImageName => "Table";

		[JsonIgnore]
		public override String Endpoint => null;

		[JsonIgnore]
		public override List<FieldNode> DefaultFields => _defaultFields;

		private readonly static List<FieldNode> _defaultFields = new List<FieldNode>()
		{
			new FieldNode() { Name = "Id", Type = FieldType.Id },
			new FieldNode() { Name = "RowNo", Type = FieldType.Integer },
			new FieldNode() { Name = "Memo", Type = FieldType.String, Length = 255 }
		};
	}
}
