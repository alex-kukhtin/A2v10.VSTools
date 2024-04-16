// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System.Windows;
using System.Windows.Controls;

namespace XamlEditor
{
	/// <summary>
	/// Interaction logic for TablePanel.xaml
	/// </summary>
	public partial class TablePanel : UserControl
	{
		private readonly ViewModel _model;
		private readonly TableNode _table;
		public TablePanel(TableNode table, ViewModel viewModel)
		{
			InitializeComponent();
			_table = table;	
			_model = viewModel;	
			DataContext = table;	
		}

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
