// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor
{
	public class UiNode : BaseNode
	{
		public IndexUiNode Index { get; set; } = new IndexUiNode();
		public Boolean ShouldSerializeIndex() => !Index.IsEmpty();
		public IndexUiNode Browse { get; set; } = new IndexUiNode();
		public Boolean ShouldSerializeBrowse() => !Browse.IsEmpty();

		public EditUiNode Edit { get; set; } = new EditUiNode();
		public Boolean ShouldSerializeEdit() => !Edit.IsEmpty();

		public Boolean IsEmpty()
		{
			return Index.IsEmpty() && Browse.IsEmpty() && Edit.IsEmpty();
		}

		private EndpointNode _endpoint;
		internal void SetParent(EndpointNode endpoint)
		{
			_endpoint = endpoint;
			Index.SetParent(endpoint);
			Browse.SetParent(endpoint);
			Edit.SetParent(endpoint);
		}
		internal void OnTableChanged()
		{
			Index.OnTableChanged();
			Browse.OnTableChanged();
			Edit.OnTableChanged();
		}

		public virtual void Clear()
		{
			Index.Clear();
			Browse.Clear();
			Edit.Clear();
		}
	}
}
