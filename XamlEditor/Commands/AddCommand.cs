// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Input;

namespace XamlEditor
{
	internal class AddCommand : ICommand
	{
		private readonly ViewModel _viewModel;
		public AddCommand(ViewModel viewModel)
		{
			_viewModel = viewModel;
		}
		public event EventHandler CanExecuteChanged;

		public void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool CanExecute(Object parameter) => _viewModel.Root.Count > 0;

		public void Execute(Object parameter)
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
			}
		}
	}
}
