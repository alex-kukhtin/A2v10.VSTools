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

	public void SetDefault(Boolean isDefault)
	{
		if (isDefault)
			Fields.Clear();
		else
		{
			foreach (var f in _table.DefaultFields)
				Fields.Add(new UiFieldNode() { Name = f.Name });

			foreach (var f in _table.Fields)
				Fields.Add(new UiFieldNode() { Name = f.Name });

			foreach (var f in Fields)
				f.SetParent(_endpoint);
		}
		OnPropertyChanged(nameof(IsDefault));
	}
}
