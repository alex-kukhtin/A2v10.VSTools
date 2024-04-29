// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows;
using Newtonsoft.Json;

namespace XamlEditor
{
	public enum SearchType
	{
		None,
		Like,
		Exact
	}
	public class UiFieldNode : BaseNode
	{
		#region Properties
		private String _title;

		[JsonProperty(Order = 1)]
		public String Title { get => _title; set { _title = value; OnPropertyChanged(); } }

		private Boolean _sort;

		[JsonProperty(Order = 2)]
		public Boolean Sort { get => _sort; set { _sort = value; OnPropertyChanged(); } }

		private SearchType _search;

		[JsonProperty(Order = 3)]
		public SearchType Search { get => _search; set { _search = value; OnPropertyChanged(); } }

		private Boolean _filter;
		[JsonProperty(Order = 4)]
		public Boolean Filter { get => _filter; set { _filter = value; OnPropertyChanged(); } }

		private Boolean _multiline;
		[JsonProperty(Order = 5)]
		public Boolean Multiline { get => _multiline; set { _multiline = value; OnPropertyChanged(); } }

		private Boolean _fit;
		[JsonProperty(Order = 6)]
		public Boolean Fit { get => _fit; set { _fit = value; OnPropertyChanged(); } }

		[JsonProperty(Order = 7)]
		public Int32 LineClamp { get => String.IsNullOrEmpty(_clamp) ? 0 : Int32.Parse(_clamp); } 

		private String _clamp;
		[JsonIgnore]
		public String Clamp { get => _clamp; 
			set { 
				if (Int32.TryParse(value, out Int32 result))
					_clamp =  result != 0 ? result.ToString() : null; 
				else
					_clamp = null;
				OnPropertyChanged(); 
			} 
		}

		private String _display;

		[JsonProperty(Order = 8)]
		public String Display { get => _display; set { _display = value; OnPropertyChanged(); } }
		#endregion

		EndpointNode _endpoint;
		FieldNode _baseField;

		internal void SetParent(EndpointNode endpoint)
		{
			_endpoint = endpoint;
			_baseField = _endpoint.FindField(Name);
		}

		[JsonIgnore]
		public Visibility IsDisplayVisible
		{
			get
			{
				FieldNode baseNode = _endpoint.FindField(Name);
				return baseNode != null && baseNode.IsReference ? Visibility.Visible : Visibility.Hidden;	
			}
		}

		[JsonIgnore]
		public Boolean CanSort => _baseField != null && _baseField.CanSort;

		[JsonIgnore]
		public Boolean CanSearch => _baseField != null && _baseField.CanSearch;

		[JsonIgnore]
		public Boolean IsReference => _baseField != null && _baseField.IsReference;

		[JsonIgnore]
		public Boolean HasClamp => _baseField != null && _baseField.HasClamp;
	}
}
