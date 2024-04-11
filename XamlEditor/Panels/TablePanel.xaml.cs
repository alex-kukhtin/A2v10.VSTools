using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XamlEditor
{
	/// <summary>
	/// Interaction logic for TablePanel.xaml
	/// </summary>
	public partial class TablePanel : UserControl
	{
		private readonly ViewModel _model;
		public TablePanel(TableNode table, ViewModel viewModel)
		{
			InitializeComponent();
			_model = viewModel;	
			DataContext = table;	
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{

		}
	}
}
