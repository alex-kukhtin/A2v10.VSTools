﻿// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

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
		public Boolean ShouldSerializeRef() => IsReference;

		[JsonIgnore]
		public Boolean HasLength  => _type == FieldType.String;
		[JsonIgnore]
		public Boolean IsReference => _type == FieldType.Reference;

		[JsonIgnore]
		public Boolean CanSort => _type != FieldType.Reference;
		[JsonIgnore]
		public Boolean CanSearch => _type != FieldType.Reference;
		[JsonIgnore]
		public Boolean HasClamp => _type == FieldType.String && Length >= Constants.ClampThreshold;

		#region IDataErrorInfo
		[JsonIgnore]
		public String Error => throw new NotImplementedException();

		[JsonIgnore]
		public String this[String columnName] {
			get
			{
				if (columnName == "Ref")
					return String.IsNullOrEmpty(Ref) ? "Required" : String.Empty;
				return String.Empty;
			}
		}
		#endregion
	}
}
