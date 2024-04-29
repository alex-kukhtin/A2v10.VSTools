// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows;
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

		private void AddParameter_Click(object sender, RoutedEventArgs e)
		{
			_endpoint.ParametersList.Add(new KeyValue() { Key = $"Key{_endpoint.ParametersList.Count + 1}"});
		}
		private void DeleteParameter_Click(object sender, RoutedEventArgs e)
		{
			if (!(e.Source is Button btnObj) || !(btnObj.CommandParameter is KeyValue kv))
				return;
			_endpoint.ParametersList.Remove(kv);
		}
	}
}
