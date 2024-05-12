// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor;

public class CreateSampleCommand(ViewModel _viewModel) : AppMenuCommand
{
	public override void Execute(Object parameter)
	{
		if (parameter != null) 
			_viewModel.CreateSampleApplication(parameter.ToString());
	}
}
