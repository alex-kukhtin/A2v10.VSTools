// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamlEditor
{
	public class JournalNode : TableNode
	{
		public JournalNode(AppNode root)
		{
			_root = root;
		}
		[JsonIgnore]
		protected override String ImageName => "Journal";
		protected override String ParentName => "journal";

		[JsonIgnore]
		public override List<FieldNode> DefaultFields => _defaultFields;

		private readonly static List<FieldNode> _defaultFields = new List<FieldNode>()
		{
			new FieldNode() { Name = "Id", Type= FieldType.Id },
			new FieldNode() { Name = "Date", Type = FieldType.Date }
		};
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
}
