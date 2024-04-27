// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class BaseUiNode : ObservableNode
	{
		[JsonProperty(Order = 2)]
		public ObservableCollection<UiFieldNode> Fields { get; set; } = new ObservableCollection<UiFieldNode>();
		public Boolean ShouldSerializeFields() => Fields.Count > 0;

		public Boolean IsEmpty()
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
			}
		}
	}
}
