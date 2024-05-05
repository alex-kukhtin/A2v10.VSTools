// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

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
		public override List<FieldNode> DefaultFields => DefaultTableFields.DocumentFields;

		[JsonIgnore]
		public override bool HasApply => true;
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
