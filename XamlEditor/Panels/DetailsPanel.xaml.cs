// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace XamlEditor
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	public partial class DetailsPanel : UserControl
	{
		private readonly ViewModel _model;
		private readonly DetailsNode _table;
		public DetailsPanel(DetailsNode table, ViewModel viewModel)
		{
			InitializeComponent();
			_table = table;
			_model = viewModel;
			DataContext = table;
		}
		public IEnumerable<String> RefTables => _model.RefTables;

		private void AddField_Click(object sender, RoutedEventArgs e)
		{
			_table.CreateField();
		}

		private void DeleteField_Click(object sender, RoutedEventArgs e)
		{
			if (!(e.Source is Button btnObj) || !(btnObj.CommandParameter is FieldNode fn))
				return;
			_table.Fields.Remove(fn);
		}
	}
}
