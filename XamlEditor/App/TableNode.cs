// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


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
		internal virtual void ApplyDefaults()
		{
		}

		[JsonIgnore]
		public String Endpoint => $"/{ParentName}/{Name.Singular().ToLowerInvariant()}";

		[JsonIgnore]
		protected virtual String ParentName => String.Empty;

		public override void OnNameChanged()
		{
			OnPropertyChanged(nameof(Endpoint));
		}
	}

}
