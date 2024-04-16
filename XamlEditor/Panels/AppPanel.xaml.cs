// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System.Windows.Controls;

namespace XamlEditor
{
	/// <summary>
	/// Interaction logic for AppPanel.xaml
	/// </summary>
	public partial class AppPanel : UserControl
	{
		public AppPanel(AppNode app)
		{
			InitializeComponent();
			DataContext = app;
		}
	}
}
