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
			if (values[0] is AppNode appNode)
				return new AppPanel(appNode);
			else if (values[0] is EndpointNode endpointNode)
				return new EndpointPanel(endpointNode, vm);
			else if (values[0] is DetailsNode detailsNode)
				return new DetailsPanel(detailsNode, vm);
			else if (values[0] is TableNode tableNode)
				return new TablePanel(tableNode, vm);
			return null;
		}

		public Object[] ConvertBack(Object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
