// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor
{
	public class DeleteCommand : AppMenuCommand
	{
		private readonly ViewModel _viewModel;
		public DeleteCommand(ViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public override bool CanExecute(Object parameter) => _viewModel.SelectedItem != null && _viewModel.SelectedItem is TableNode;

		public override void Execute(Object parameter)
		{
			if (!CanExecute(parameter))
				return;
			if (_viewModel.SelectedItem is TableNode tableNode) 
				_viewModel.AppNode.DeleteTable(tableNode);
		}
	}
}
