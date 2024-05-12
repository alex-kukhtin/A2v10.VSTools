// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace XamlEditor;

public class DetailsNode : TableNode
{
	private TableNode _parentTable;
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
	public override String Schema => _parentTable.Schema;

	[JsonIgnore]
	public override List<FieldNode> DefaultFields => GetDefaultFields();

	private List<FieldNode> GetDefaultFields() {
		List<FieldNode> fields = 
		[
			new() { Name = "Id", Type = FieldType.Id },
			new() { Name = "RowNo", Type = FieldType.Integer }
		];
		fields.Add(new() { 
			Name = _parentTable.Name.Singular(), 
			Type = FieldType.Parent, 
			Ref = $"{_parentTable.ParentName}.{_parentTable.Name}"
		});
		fields.Add(new FieldNode()
			{ 
				Name = "Memo", Type = FieldType.String, Length = 255 
			}
		);
		return fields;
	}

	public void OnInit(AppNode root, TableNode parent)
	{
		base.OnInit(root);
		_parentTable = parent;
	}
}
