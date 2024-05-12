// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System.Collections.Generic;

namespace XamlEditor;

internal static class DefaultTableFields
{
	internal readonly static List<FieldNode> DocumentFields = 
		[
			new () { Name = "Id", Type= FieldType.Id },
			new () { Name = "Done", Type = FieldType.Boolean },
			new () { Name = "Number", Length = 32 },
			new () { Name = "Date", Type = FieldType.Date },
			new () { Name = "Sum", Type = FieldType.Money },
			new () { Name = "Memo", Length = 255 }
		];

	internal readonly static List<FieldNode> CatalogFields = 
		[
			new () { Name = "Id", Type = FieldType.Id },
			new () { Name = "Void", Type = FieldType.Boolean },
			new () { Name = "Name", Length = 255 },
			new () { Name = "Memo", Length = 255 }
		];

	internal readonly static List<FieldNode> JournalFields = 
		[
			new () { Name = "Id", Type = FieldType.Id },
			new () { Name = "Date", Type = FieldType.Date, Required = true },
			new () { Name = "InOut", Type = FieldType.Integer, Required = true }
		];
}
