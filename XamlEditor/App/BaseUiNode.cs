// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor;

public class BaseUiNode : ObservableNode
{
	[JsonProperty(Order = 1)]
	public ObservableCollection<UiFieldNode> Fields { get; set; } = [];
	public Boolean ShouldSerializeFields() => Fields.Count > 0;

	[JsonIgnore]
	public virtual Boolean IsDefault => this.Fields.Count == 0; // NOT IsEmpty!

	public virtual Boolean IsEmpty()
	{
		return this.Fields.Count == 0;
	}
	public void SetDefault(TableNode tableNode)
	{
		if (tableNode == null)
			Fields.Clear();
		else
		{
			foreach (var f in tableNode.DefaultFields)
				Fields.Add(new UiFieldNode() { Name = f.Name });

			foreach (var f in tableNode.Fields)
				Fields.Add(new UiFieldNode() { Name = f.Name });

			foreach (var f in Fields)
				f.SetParent(_endpoint);
		}
		OnPropertyChanged(nameof(IsDefault));
	}

	protected EndpointNode _endpoint;
	internal virtual void SetParent(EndpointNode endpoint)
	{
		_endpoint = endpoint;
		foreach (var f in Fields)
			f.SetParent(endpoint);
	}
	internal virtual void OnTableChanged()
	{
	}

	[JsonIgnore]
	public virtual IEnumerable<String> RefFields => GetRefFields(_endpoint.GetTable());

	public IEnumerable<String> GetRefFields(TableNode table)
	{
		if (table != null)
		{
			foreach (var field in table.DefaultFields)
				yield return field.Name;
			foreach (var field in table.Fields)
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
	}
}
