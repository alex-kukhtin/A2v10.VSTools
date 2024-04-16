// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class TablesNode : BaseNode
	{
		private readonly ObservableCollection<TableNode> _tables;
		public TablesNode(ObservableCollection<TableNode> tables)
		{
			Name = "Tables";
			_tables = tables;
		}
		public override IEnumerable<BaseNode> Children => _tables;
	}

	public class TableNode : BaseNode
	{
		[JsonProperty(Order = 2)]
		public ObservableCollection<FieldNode> Fields { get; set; } = new ObservableCollection<FieldNode>();
		public Boolean ShouldSerializeFields() => Fields != null && Fields.Count > 0;

		[JsonProperty(Order = 3)]
		public ObservableCollection<TableNode> Details { get; set; } = new ObservableCollection<TableNode>();
		public Boolean ShouldSerializeDetails() => Details != null && Details.Count > 0;

		[JsonIgnore]
		public virtual List<FieldNode> DefaultFields { get; }

		internal virtual void ApplyDefaults()
		{
		}

		public String Endpoint => $"/{ParentName}/{Name.Singular().ToLowerInvariant()}";

		[JsonIgnore]
		protected virtual String ParentName => String.Empty;

		public override void OnNameChanged()
		{
			OnPropertyChanged(nameof(Endpoint));
		}

		public void CreateField()
		{
			var f = new FieldNode() { Name = $"Field{Fields.Count + 1}" };
			Fields.Add(f);
		}
	}

}
