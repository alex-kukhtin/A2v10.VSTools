// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Controls;

namespace XamlEditor;


public class SqlScriptViewModel(AppNode _root)
{
	public String CreateTableScript => new SqlTextBuilder().BuildTables(_root);
}

public partial class SqlScriptPanel : UserControl
{
	private readonly AppNode _root;
	public SqlScriptPanel(AppNode root)
	{
		_root = root;
		InitializeComponent();
		DataContext = new SqlScriptViewModel(root);	
	}
}
