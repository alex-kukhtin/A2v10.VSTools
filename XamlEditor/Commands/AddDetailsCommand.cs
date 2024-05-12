// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor;

public class AddDetailsCommand : AppMenuCommand
{
	private readonly ViewModel _viewModel;
	public AddDetailsCommand(ViewModel viewModel)
	{
		_viewModel = viewModel;
	}

	public override bool CanExecute(Object parameter)  => SelectedNode() != null;

	TableNode SelectedNode()
	{
		if (_viewModel.SelectedItem is TableNode tableNode && _viewModel.SelectedItem is not JournalNode)
			return tableNode;
		return null;
	}
	public override void Execute(Object parameter)
	{
		var selNode = SelectedNode();
		selNode?.AddDetails();
	}
}
