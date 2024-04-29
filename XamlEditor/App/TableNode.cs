﻿// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
		public ObservableCollection<DetailsNode> Details { get; set; } = new ObservableCollection<DetailsNode>();
		public Boolean ShouldSerializeDetails() => Details != null && Details.Count > 0;

		[JsonIgnore]
		public virtual List<FieldNode> DefaultFields { get; }

		[JsonIgnore]
		public override IEnumerable<BaseNode> Children => Details;

		internal virtual void ApplyDefaults()
		{
		}

		public virtual String Endpoint => $"/{ParentName}/{Name.Singular().ToLowerInvariant()}";

		[JsonIgnore]
		protected virtual String ParentName => String.Empty;

		public override void OnNameChanged()
		{
			OnPropertyChanged(nameof(Endpoint));
		}

		public void CreateField()
		{
			var f = new FieldNode() { Name = $"Field{Fields.Count + 1}", Length = 255 };
			Fields.Add(f);
		}

		public void AddDetails()
		{
			var details = new DetailsNode(_root, this) { Name = $"Details{Details.Count + 1}" };
			Details.Add(details);
			details.IsSelected = true;
		}

		public FieldNode FindField(String Name) {
			var r = Fields.FirstOrDefault(f => f.Name == Name);
			if (r != null)
				return r;
			return DefaultFields.FirstOrDefault(f => f.Name == Name);
		}
	}
}
