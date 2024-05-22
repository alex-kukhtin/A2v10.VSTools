// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
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
			ParametersList.CollectionChanged += CollectionChanged;
		}
		[JsonIgnore]
		protected override String ImageName => "EntryPoint";

		private String _table;
		[JsonProperty(Order = 1)]
		public String Table { get => _table; set { _table = value; OnPropertyChanged(); OnTableChanged(); } }

		private String _title;
		[JsonProperty(Order = 2)]
		public String Title { get => _title; set { _title = value; OnPropertyChanged(); OnTableChanged(); } }

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

		[JsonProperty(Order = 8)]
		public UiNode UI { get; set; } = new UiNode();
		public Boolean ShouldSerializeUI() => !UI.IsEmpty();

		[JsonProperty(Order = 10)]
		public ObservableCollection<ApplyNode> Apply { get; set; } = [];
		public Boolean ShouldSerializeApply() => Apply.Count > 0;

		public Boolean HasApply => GetTable()?.HasApply ?? false;

		private void OnTableChanged()
		{
			if (Name.StartsWith("Endpoint"))
			{
				var tbl = _root.FindNode(Table);
				if (tbl != null)
					Name = tbl.Endpoint;
				else
					Name = Table;
			}
			OnPropertyChanged(String.Empty);
			UI.OnTableChanged();
		}

		internal override void OnInit(AppNode root)
		{
			base.OnInit(root);
			_root = root;
			UI.OnInit(root);
			UI.SetParent(this);
			foreach (var apply in Apply)
			{
				apply.OnInit(root);
				apply.SetParent(this);
			}
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
		private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (_root != null)
				_root.IsDirty = true;
		}

		internal Boolean IsParameter(FieldNode field)
		{
			return ParametersList != null && Parameters.ContainsKey(field.Name);
		}

		[JsonIgnore]
		public IEnumerable<String> ParamFields => FindTable(Table).Fields.Select(s => s.Name);

		public void Clear()
		{
			UI.Clear();
			Apply.Clear();
			OnPropertyChanged(String.Empty);
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
