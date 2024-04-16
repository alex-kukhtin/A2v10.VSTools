﻿// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace XamlEditor
{
	public class DocumentNode : TableNode
	{
		public DocumentNode(AppNode root)
		{
			_root = root;
		}
		[JsonIgnore]
		protected override String ImageName => "Document";
		protected override String ParentName => "document";

		[JsonIgnore]
		public override List<FieldNode> DefaultFields => _defaultFields;

		private readonly static List<FieldNode> _defaultFields = new List<FieldNode>()
		{
			new FieldNode() { Name = "Id" },
			new FieldNode() { Name = "Done" },
			new FieldNode() { Name = "Date" },
			new FieldNode() { Name = "Number" },
			new FieldNode() { Name = "Memo" }
		};
	}

	public class DocumentsNode : BaseNode
	{
		private readonly ObservableCollection<DocumentNode> _documents;
		public DocumentsNode(ObservableCollection<DocumentNode> documents)
		{
			Name = "Documents";
			_documents = documents;
		}
		public override IEnumerable<BaseNode> Children => _documents;

	}
}