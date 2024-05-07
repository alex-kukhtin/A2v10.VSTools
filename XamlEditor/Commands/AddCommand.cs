// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor;

internal class AddCommand(ViewModel _viewModel) : AppMenuCommand
{
	public override bool CanExecute(Object parameter) => _viewModel.Root.Count > 0;

	public override void Execute(Object parameter)
	{
		var root = _viewModel.RootNode;
		switch (parameter?.ToString())
		{
			case "Catalog":
				root.AddCatalog();
				break;
			case "Document":
				root.AddDocument();
				break;
			case "Journal":
				root.AddJournal(); 
				break;
			case "Endpoint":
				root.AddEndpoint(); 
				break;
		}
	}
}
