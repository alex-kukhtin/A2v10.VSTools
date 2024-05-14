// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Controls;

namespace XamlEditor;


public class SqlScriptViewModel(AppNode _root)
{
	public String CreateTableScript => new SqlTextBuilder(_root).BuildTables();
	public String CreateUiScript => new SqlTextBuilder(_root).BuildUI();
}

public partial class SqlScriptPanel : UserControl
{
	public SqlScriptPanel(AppNode root)
	{
		InitializeComponent();
		DataContext = new SqlScriptViewModel(root);	
	}
}
