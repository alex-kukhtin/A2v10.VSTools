// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class EndpointNode : BaseNode
	{
		public EndpointNode(AppNode root) 
		{
			_root = root;
		}
		[JsonIgnore]
		protected override String ImageName => "NewDocument";

		private String _table;
		[JsonProperty(Order = 1)]
		public String Table { get => _table; set { _table = value; OnPropertyChanged(); OnTableChanged(); } }

		private String _title;
		[JsonProperty(Order = 2)]
		public String Title { get => _title; set { _title = value; OnPropertyChanged(); OnTableChanged(); } }

		[JsonProperty(Order = 5)]
		public UiNode UI { get; set; } = new UiNode();
		public Boolean ShouldSerializeUI() => !UI.IsEmpty();

		[JsonProperty(Order = 6)]
		public Dictionary<String, String> Parameters { get; set; } = new Dictionary<String, String>();

		private void OnTableChanged()
		{
			if (Name.StartsWith("Endpoint"))
				Name = Table;
			OnPropertyChanged(String.Empty);
			UI.OnTableChanged();
		}

		internal void OnInit(AppNode root)
		{
			_root = root;
			UI.SetParent(this);
		}

		internal FieldNode FindField(String Name)
		{
			var table = _root.FindNode(Table);
			return table.FindField(Name);
		}

		internal TableNode GetTable()
		{
			if (String.IsNullOrEmpty(Table))
				return null;
			return _root.FindNode(Table);
		}
	}

	public class EndpointsNode : BaseNode
	{
		private readonly ObservableCollection<EndpointNode> _endpoints;
		public EndpointsNode(ObservableCollection<EndpointNode> endpoints)
		{
			Name = "Endpoints";
			_endpoints = endpoints;
		}
		public override IEnumerable<BaseNode> Children => _endpoints;
	}
}
