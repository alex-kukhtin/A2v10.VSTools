// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamlEditor
{
	public class EditUiNode : BaseUiNode
	{
		public String Name { get; set; }
		public ObservableCollection<DetailsUiNode> Details { get; set; } = new ObservableCollection<DetailsUiNode>();
		public void CreateDetails()
		{
			if (_endpoint == null)
				return;
			var table = _endpoint.GetTable();
			Details.Clear();
			if (table == null)
				return;
			foreach (var dd in table.Details)
			{
				var node = new DetailsUiNode();
				node.SetParent(_endpoint, dd);
				Details.Add(node);
			}
		}

		public override bool IsEmpty()
		{
			return base.IsEmpty() && Details.All(d => d.IsEmpty());
		}

		internal override void SetParent(EndpointNode endpoint)
		{
			base.SetParent(endpoint);
			OnTableChanged();
		}

		internal override void OnTableChanged()
		{
			base.OnTableChanged();
			CreateDetails();	
		}
	}
}
