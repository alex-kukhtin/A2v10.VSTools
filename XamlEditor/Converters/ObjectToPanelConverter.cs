// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Globalization;
using System.Windows.Data;

namespace XamlEditor
{
	internal class ObjectToPanelConverter : IMultiValueConverter
	{
		public Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
		{
			var vm = values[1] as ViewModel ?? throw new InvalidOperationException("Invalid Converter parameter");
			return values[0] switch
			{
				AppNode appNode => new AppPanel(appNode),
				EndpointNode endpointNode => new EndpointPanel(endpointNode, vm),
				DetailsNode detailsNode => new DetailsPanel(detailsNode, vm),
				TableNode tableNode => new TablePanel(tableNode, vm),
				MenuNode menuNode => new MenuPanel(menuNode, vm),
				SqlScriptNode sqlScriptNode => new SqlScriptPanel(vm.RootNode),
				_ => null
			};
		}

		public Object[] ConvertBack(Object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
