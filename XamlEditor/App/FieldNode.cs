// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public enum FieldType
	{
		String,
		Id,
		Boolean,
		Date,
		DateTime,
		Integer,
		Float,
		Money,
		Guid,
		Reference
	}

	public class FieldNode : BaseNode, IDataErrorInfo
	{
		private String _title;

		[JsonProperty(Order = 1)]
		public String Title { get => _title; set { _title = value; OnPropertyChanged(); } }

		private FieldType _type;
		[JsonProperty(Order = 4)]
		public FieldType Type { get => _type; set { _type = value; OnPropertyChanged(String.Empty); } }

		private Int32 _length = 255;
		[JsonProperty(Order = 5)]
		public Int32 Length { get => _length; set { _length = value; OnPropertyChanged(); } }
		public Boolean ShouldSerializeLength() => _type == FieldType.String && _length != 255;

		private String _ref;
		[JsonProperty(Order = 6)]
		public String Ref { get => _ref; set { _ref = value; OnPropertyChanged(); } }
		public Boolean ShouldSerializeRef() => HasRef;

		[JsonIgnore]
		public Boolean HasLength  => _type == FieldType.String;
		[JsonIgnore]
		public Boolean HasRef => _type == FieldType.Reference;

		#region IDataErrorInfo
		[JsonIgnore]
		public String Error => throw new NotImplementedException();

		[JsonIgnore]
		public String this[String columnName] => String.Empty;
		#endregion
	}
}
