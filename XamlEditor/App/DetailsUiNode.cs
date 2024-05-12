// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;

namespace XamlEditor;

public class DetailsUiNode : BaseUiNode
{
	public String Name { get; set; }

	private TableNode _table;
	internal void SetParent(EndpointNode endpoint, TableNode table)
	{
		base.SetParent(endpoint);
		_table = table;
		Name = _table.Name;
	}

	public override IEnumerable<String> RefFields => GetRefFields(_table);

	public override void ApplyDefaults(TableNode table = null)
	{
		var def = DefaultUiFields.DefaultUiDetailsElement(_table);
		foreach (var f in def)
			Fields.Add(f);
	}
	public override void SetDefault(bool isDefault, TableNode table = null)
	{
		base.SetDefault(isDefault, _table);
	}
}
