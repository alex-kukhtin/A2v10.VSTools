// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor;

public class BaseUiNode : ObservableNode
{
	public BaseUiNode()
	{
		Fields.CollectionChanged += CollectionChanged;
	}

	private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
	{
		if (_root != null)
			_root.IsDirty = true;
	}

	[JsonProperty(Order = 1)]
	public ObservableCollection<UiFieldNode> Fields { get; set; } = [];
	public Boolean ShouldSerializeFields() => Fields.Count > 0;

	[JsonIgnore]
	public virtual Boolean IsDefault => this.Fields.Count == 0; // NOT IsEmpty!

	public virtual Boolean IsEmpty()
	{
		return this.Fields.Count == 0;
	}

	public virtual void ApplyDefaults(TableNode table = null)
	{
	}

	public virtual void SetDefault(Boolean isDefault, TableNode table = null)
	{
		if (isDefault)
			Fields.Clear();
		else
		{
			ApplyDefaults(table);
			foreach (var f in Fields)
				f.SetParent(_endpoint);
		}
		OnPropertyChanged(nameof(IsDefault));
	}

	protected EndpointNode _endpoint;
	internal virtual void SetParent(EndpointNode endpoint)
	{
		_endpoint = endpoint;
		_root = endpoint._root;	
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
