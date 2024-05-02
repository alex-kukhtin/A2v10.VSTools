using System;
using System.Text;

namespace XamlEditor;

internal class SqlTextBuilder
{
	public String BuildTables(AppNode node)
	{
		StringBuilder builder = new();
		foreach (var t in node.Catalogs)
			builder.AppendLine(BuildTable(t));
		foreach (var t in node.Documents)
			builder.AppendLine(BuildTable(t));
		foreach (var t in node.Journals)
			builder.AppendLine(BuildTable(t));
		return builder.ToString();
	}

	public String BuildTableTypes(AppNode node)
	{
		StringBuilder builder = new();
		foreach (var t in node.Catalogs)
			builder.AppendLine(BuildTableType(t));
		foreach (var t in node.Documents)
			builder.AppendLine(BuildTableType(t));
		return builder.ToString();
	}

	String BuildTable(TableNode table)
	{
		var tableName = table.Name;
		var schema = "cat"; // TODO
		return $""""
		------------------------------------------------
		if not exists(select * from INFORMATION_SCHEMA.SEQUENCES where SEQUENCE_SCHEMA = N'{schema}' and SEQUENCE_NAME = N'SQ_{tableName}')
			create sequence {schema}.SQ_{tableName} as bigint start with 100 increment by 1;
		go
		------------------------------------------------
		create table {schema}.[{tableName}
		(
		);
		go
		"""";
	}

	String BuildTableType(TableNode table)
	{
		var tableName = table.Name;
		var schema = "cat"; // TODO
		return $""""
		------------------------------------------------
		drop type if exists {schema}.[{tableName}.TableType];
		go
		------------------------------------------------
		create type {schema}.[{tableName}.TableType] as table
		(
		)
		go
		"""";
	}
}
