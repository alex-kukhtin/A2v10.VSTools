// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace XamlEditor
{
	public partial class EndpointPanel : UserControl
	{
		private readonly ViewModel _model;
		private readonly EndpointNode _endpoint;
		public EndpointPanel(EndpointNode endpoint, ViewModel viewModel)
		{
			InitializeComponent();
			_endpoint = endpoint;	
			_model = viewModel;	
			DataContext = _endpoint;	
		}

		public IEnumerable<String> RefTables => _model.RefTables;
	}
}
