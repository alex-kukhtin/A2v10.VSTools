// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor
{
	public class DetailsUiNode : BaseUiNode
	{
		public String Name { get { return _table.Name; } }

		private TableNode _table;
		internal void SetParent(EndpointNode endpoint, TableNode table)
		{
			base.SetParent(endpoint);
			_table = table;
		}

		public void SetDefault(Boolean isDefault)
		{
			if (isDefault)
				Fields.Clear();
			else
			{
				foreach (var f in _table.DefaultFields)
					Fields.Add(new UiFieldNode() { Name = f.Name });

				foreach (var f in _table.Fields)
					Fields.Add(new UiFieldNode() { Name = f.Name });

				foreach (var f in Fields)
					f.SetParent(_endpoint);
			}
			OnPropertyChanged(nameof(IsDefault));
		}
	}
}
