// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class FieldNode : BaseNode, IDataErrorInfo
	{
		#region
		[JsonIgnore]
		public String Error => throw new NotImplementedException();

		[JsonIgnore]
		public String this[String columnName] => String.Empty;
		#endregion
	}
}
