// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

namespace XamlEditor;

internal class JsonScriptNode : BaseNode
{
	public JsonScriptNode(AppNode root)
	{
		_root = root;
		Name = "JSON source";
	}
	protected override string ImageName => "JsonFile";
}
