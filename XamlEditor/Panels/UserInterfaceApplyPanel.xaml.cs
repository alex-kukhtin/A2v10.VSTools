// Copyright © 2022-2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace XamlEditor;

public partial class UserInterfaceApplyPanel : UserControl
{
	private readonly EndpointNode _endpoint;
	public UserInterfaceApplyPanel(EndpointNode endpoint)
	{
		_endpoint = endpoint;
		InitializeComponent();
		DataContext = endpoint;
	}

	public IEnumerable<String> SourceJournals => _endpoint._root.Journals.Select(x => x.Name).ToList();

	public IEnumerable<String> SourceDetails 
	{
		get
		{
			var tbl = _endpoint.GetTable();
			var itmName = tbl.Name.Singular();
			yield return itmName;
			foreach (var details in tbl.Details)
				yield return $"{itmName}.{details.Name}";
		}
	}
}
