// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

using System.Windows.Input;

namespace XamlEditor
{
	public abstract class AppMenuCommand : ICommand
	{
		public AppMenuCommand() 
		{ 
		}

		public event EventHandler CanExecuteChanged;

		public void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public abstract bool CanExecute(object parameter);

		public abstract void Execute(object parameter);
	}
}
