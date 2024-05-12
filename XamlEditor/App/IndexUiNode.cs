// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

namespace XamlEditor;

public class IndexUiNode : BaseUiNode
{
	internal override void SetParent(EndpointNode endpoint)
	{
		base.SetParent(endpoint);
	}

	public override void ApplyDefaults(TableNode table = null)
	{
		var def = DefaultUiFields.DefaultUiIndexElement(_endpoint);
		foreach (var f in def)
			Fields.Add(f);
	}
}
