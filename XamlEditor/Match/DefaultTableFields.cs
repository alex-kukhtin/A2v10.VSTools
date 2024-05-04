// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System.Collections.Generic;

namespace XamlEditor;

internal static class DefaultTableFields
{
	internal readonly static List<FieldNode> DocumentFields = new List<FieldNode>()
		{
			new FieldNode() { Name = "Id", Type= FieldType.Id },
			new FieldNode() { Name = "Done", Type = FieldType.Boolean },
			new FieldNode() { Name = "Number", Length = 32 },
			new FieldNode() { Name = "Date", Type = FieldType.Date },
			new FieldNode() { Name = "Sum", Type = FieldType.Money },
			new FieldNode() { Name = "Memo", Length = 255 }
		};

	internal readonly static List<FieldNode> CatalogFields = new List<FieldNode>()
		{
			new FieldNode() { Name = "Id", Type = FieldType.Id },
			new FieldNode() { Name = "Void", Type = FieldType.Boolean },
			new FieldNode() { Name = "Name", Length = 255 },
			new FieldNode() { Name = "Memo", Length = 255 }
		};

	internal readonly static List<FieldNode> JournalFields = new List<FieldNode>()
		{
			new FieldNode() { Name = "Id", Type = FieldType.Id },
			new FieldNode() { Name = "Date", Type = FieldType.Date },
		};
}
