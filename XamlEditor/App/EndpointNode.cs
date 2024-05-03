// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace XamlEditor
{
	public class KeyValue : ObservableNode
	{
		public String Key { get; set; }
		public String Value { get; set; }
	}
	public class EndpointNode : BaseNode
	{
		public EndpointNode(AppNode root) 
		{
			_root = root;
		}
		[JsonIgnore]
		protected override String ImageName => "EntryPoint";

		private String _table;
		[JsonProperty(Order = 1)]
		public String Table { get => _table; set { _table = value; OnPropertyChanged(); OnTableChanged(); } }

		private String _title;
		[JsonProperty(Order = 2)]
		public String Title { get => _title; set { _title = value; OnPropertyChanged(); OnTableChanged(); } }

		[JsonProperty(Order = 5)]
		public UiNode UI { get; set; } = new UiNode();
		public Boolean ShouldSerializeUI() => !UI.IsEmpty();

		private readonly ObservableCollection<KeyValue> _parameters = [];

		[JsonIgnore]
		public ObservableCollection<KeyValue> ParametersList => _parameters;

		[JsonProperty(Order = 6)]
		public Dictionary<String, String> Parameters
		{
			get
			{
				var dict = new Dictionary<String, String>();	
				foreach (var kvp in  _parameters)	
					dict.Add(kvp.Key, kvp.Value);	
				return dict;
			}
			set
			{
				_parameters.Clear();
				foreach (var kvp in value)
					_parameters.Add(new KeyValue() { Key = kvp.Key, Value = kvp.Value});
			}
		}
		public Boolean ShouldSerializeParameters() => ParametersList.Count > 0;

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

		internal TableNode FindTable(String name)
		{
			return _root.FindNode(name);
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
