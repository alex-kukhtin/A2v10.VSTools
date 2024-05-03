// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;

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

	public IEnumerable<String> RefFields()
	{
		foreach (var field in _table.DefaultFields)
			yield return field.Name;
		foreach (var field in _table.Fields)
		{
			yield return field.Name;
			if (field.IsReference)
			{
				var refTable = _endpoint.FindTable(field.Ref);
				foreach (var rfield in refTable.Fields)
					yield return $"{field.Name}.{rfield.Name}";
			}
		}
	}

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
