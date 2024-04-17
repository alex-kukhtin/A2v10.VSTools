// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor
{
	internal class AddCommand : AppMenuCommand
	{
		private readonly ViewModel _viewModel;
		public AddCommand(ViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public override bool CanExecute(Object parameter) => _viewModel.Root.Count > 0;

		public override void Execute(Object parameter)
		{
			var root = _viewModel.Root[0];
			switch (parameter?.ToString())
			{
				case "Catalog":
					root.AddCatalog();
					break;
				case "Document":
					root.AddDocument();
					break;
				case "Endpoint":
					root.AddEndpoint(); 
					break;
			}
		}
	}
}
