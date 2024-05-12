// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

using System.Windows.Input;

namespace XamlEditor;

public abstract class AppMenuCommand : ICommand
{
	public event EventHandler CanExecuteChanged;

	public void OnCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}

	public virtual bool CanExecute(object parameter) => true;

	public abstract void Execute(object parameter);
}
