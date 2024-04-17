// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace XamlEditor
{
	internal static class JsonHelpers
	{
		internal readonly static JsonSerializerSettings DefaultSettings = new JsonSerializerSettings()
		{
			Formatting = Formatting.Indented,
			NullValueHandling = NullValueHandling.Ignore,
			DefaultValueHandling = DefaultValueHandling.Ignore,
			ContractResolver = new DefaultContractResolver()
			{
				NamingStrategy = new CamelCaseNamingStrategy()
			},
			Converters = new List<JsonConverter>() {
				new StringEnumConverter()
			}
		};
	}
}
