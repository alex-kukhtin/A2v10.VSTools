// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System.Windows.Controls;

namespace XamlEditor;

public partial class JsonScriptPanel : UserControl
{
	public JsonScriptPanel(AppNode appNode)
	{
		InitializeComponent();
		DataContext = appNode;
	}
}
