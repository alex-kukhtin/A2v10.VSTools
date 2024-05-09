// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor;

public class JournalNode : TableNode
{
	public JournalNode(AppNode root)
	{
		_root = root;
	}
	[JsonIgnore]
	protected override String ImageName => "Journal";
	protected override String ParentName => "journal";
	public override String Schema => "jrn";

	[JsonIgnore]
	public override List<FieldNode> DefaultFields => DefaultTableFields.JournalFields;
}

public class JournalsNode : BaseNode
{
	private readonly ObservableCollection<JournalNode> _journals;
	public JournalsNode(ObservableCollection<JournalNode> journals)
	{
		Name = "Journals";
		_journals = journals;
	}
	public override IEnumerable<BaseNode> Children => _journals;
}
