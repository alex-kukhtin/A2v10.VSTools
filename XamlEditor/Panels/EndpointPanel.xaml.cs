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

		public UserInterfaceIndexPanel UiIndex => new UserInterfaceIndexPanel(_endpoint.UI.Index, _endpoint);
		public UserInterfaceIndexPanel BrowseIndex => new UserInterfaceIndexPanel(_endpoint.UI.Browse, _endpoint);
		public UserInterfaceEditPanel EditItem => new UserInterfaceEditPanel(_endpoint.UI.Edit, _endpoint);
	}
}
