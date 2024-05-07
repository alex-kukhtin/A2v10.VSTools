// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

namespace XamlEditor;

internal class SqlScriptNode : BaseNode
{
	public SqlScriptNode(AppNode root)
	{
		_root = root;
		Name = "SQL Script";
	}
	protected override string ImageName => "DatabaseScript";
}
