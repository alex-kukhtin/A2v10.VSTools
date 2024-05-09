using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlEditor;

internal static class SqlFieldExtensions
{
	public static String SqlTableType(this FieldNode field)
	{
		return field.Type switch
		{
			FieldType.Id or FieldType.Reference => "bigint",
			FieldType.Money => "money",
			FieldType.String => $"nvarchar({field.Length})",
			FieldType.DateTime => "datetime",
			FieldType.Date => "date",
			FieldType.Integer => "int",
			FieldType.Boolean => "bit",
			FieldType.Float => "float",
			FieldType.Guid => "uniqueidentifier",
			_ => throw new InvalidOperationException($"Invalid Field Type {field.Type}")
		};
	}

	public static String SqlCreateField(this FieldNode field, TableNode table, AppNode root)
	{
		var tableName = table.Name;
		var schema = table.Schema;
		if (field.Name == "Void")
			return $"Void bit not null constraint DF_{tableName}_Void default(0)";
		else if (field.Name == "Id")
			return $"""
			Id bigint not null
					constraint PK_{tableName} primary key
					constraint DF_{tableName}_Id default (next value for {schema}.SQ_{tableName}),			
			""";
		if (field.Type == FieldType.Reference)
		{
			var refTable = root.FindNode(field.Ref);
			return $"""
			[{field.Name}] bigint 
					constraint FK_{tableName}_{field.Name} foreign key references {refTable.Schema}[{refTable.Name}](Id)
			""";
		}
		return $"[{field.Name}] {field.SqlTableType()}";
	}
}