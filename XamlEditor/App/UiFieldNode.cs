// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class UiFieldNode : BaseNode
	{
		private String _title;

		[JsonProperty(Order = 1)]
		public String Title { get => _title; set { _title = value; OnPropertyChanged(); } }

		private Boolean _sort;

		[JsonProperty(Order = 2)]
		public Boolean Sort { get => _sort; set { _sort = value; OnPropertyChanged(); } }
	}
}
