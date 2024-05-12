// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace XamlEditor;

internal static class DefaultUiFields
{
	internal static List<UiFieldNode> DefaultUiIndexElement(EndpointNode endpoint)
	{
		var tbl = endpoint.GetTable();
		List<UiFieldNode> list = tbl.ParentName switch
		{
			"Catalog" => [
				new () { Name = "Id", Sort = true, Fit = true, Search = SearchType.Exact },
				new () { Name = "Name", Sort = true, Search = SearchType.Like, Clamp = "2" }
			],
			"Document" => [
				new () { Name = "Id", Sort = true, Fit = true, Search = SearchType.Exact },
				new () { Name = "Number", Sort = true, Fit = true, Search = SearchType.Like },
				new () { Name = "Date", Sort = true, Filter = true },
				new () { Name = "Sum", Sort = true},
			],
			_ => throw new InvalidOperationException($"Invalid table for IndexUI ({tbl.ParentName})")
		};

		foreach (var f in tbl.Fields.Where(f => !endpoint.IsParameter(f)))
        {
			list.Add(new UiFieldNode()
			{
				Name = f.Name,
				Filter = f.IsReference,
				Sort = f.CanSort,
				Search = f.CanSearch ? SearchType.Like : SearchType.None,
				Clamp = f.HasClamp ? "2" : "0",
				Fit = f.Length < Constants.FitThreshold
			});
        }
		list.Add(new UiFieldNode() { Name = "Memo", Sort = true, Search = SearchType.Like, Clamp = "2" });
        return list;
	}

	internal static List<UiFieldNode> DefaultUiEditElement(EndpointNode endpoint)
	{
		var tbl = endpoint.GetTable();
		String computedSum = null;
		if (tbl.Details?.Count == 1 && tbl.Details[0].Fields.Any(f => f.Name == "Sum"))
			computedSum = $"this.{tbl.Details[0].Name}.$sum(r => r.Sum)";

		List<UiFieldNode> list = tbl.ParentName switch
		{
			"Catalog" => [
				new() { Name = "Name", Required = true }
			],
			"Document" => [
				new() { Name = "Date" },
				new() { Name = "Number" },
				new() { Name = "Sum", Computed = computedSum }
			],
			_ => throw new InvalidOperationException($"Invalid table for EditUI ({tbl.ParentName})")
		};

		foreach (var f in tbl.Fields.Where(f => !endpoint.IsParameter(f)))
		{
			list.Add(new UiFieldNode()
			{
				Name = f.Name,
				Fit = f.IsReference
			});
		}
		list.Add(new UiFieldNode() { Name = "Memo", Multiline = true });
		return list;
	}

	internal static List<UiFieldNode> DefaultUiDetailsElement(TableNode table)
	{
		bool hasQtyPriceSum = table.Fields.Any(f => f.Name == "Qty")
			&& table.Fields.Any(f => f.Name == "Price")
			&& table.Fields.Any(f => f.Name == "Sum");

		List<UiFieldNode> list = [
			new() { Name = "RowNo" }
		];

		foreach (var f in table.Fields)
		{
			list.Add(new UiFieldNode()
			{
				Name = f.Name,
				Computed = hasQtyPriceSum && f.Name == "Sum" ? "this.Price * this.Qty" : null,
				Total = f.Name == "Sum"
			});
		}
		list.Add(new UiFieldNode() { Name = "Memo", Multiline = true });
		return list;
	}
}
