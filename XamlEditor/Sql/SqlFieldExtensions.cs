// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

using System.Collections.Generic;
using System.Linq;

namespace XamlEditor;

internal static class SqlFieldExtensions
{
	public static String SqlTableType(this FieldNode field)
	{
		return field.Type switch
		{
			FieldType.Id or FieldType.Reference or FieldType.Parent => "bigint",
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
					constraint DF_{tableName}_Id default (next value for {schema}.SQ_{tableName})
			""";
		if (field.Type == FieldType.Reference || field.Type == FieldType.Parent)
		{
			var refTable = root.FindNode(field.Ref);
			return $"""
			{field.Name.EscapeSql()} bigint 
					constraint FK_{tableName}_{field.Name}_{refTable.Name} foreign key references {refTable.Schema}.{refTable.Name.EscapeSql()}(Id)
			""";
		}
		var notNull = field.Required ? " not null" : String.Empty;
		return $"{field.Name.EscapeSql()} {field.SqlTableType()}{notNull}";
	}

	private static readonly HashSet<String> _escapeNames =
		[.. "Begin,End,Start,Name,Date,Sum,Avg,Min,Max,Rows,External,File,For,From,To,Group,Case,If,Into,Table,Tran,Union,Update,Insert,Delete,Inner,Outer,Version,Uid,Contract,"
			.Split([','], StringSplitOptions.RemoveEmptyEntries)];
	public static String EscapeSql(this String name) 
	{
		if (_escapeNames.Contains(name,StringComparer.OrdinalIgnoreCase))
			return $"[{name}]";
		return name;
	}

	public static String StringValueOrNull(this String text, String prefix = null, String suffix = null)
	{
		if (String.IsNullOrEmpty(text))
			return "null";
		return $"N'{prefix}{text}{suffix}'";
	}
	public static String IntValueOrNull(this Int32 val)
	{
		if (val == 0)
			return "null";
		return val.ToString();
	}
}