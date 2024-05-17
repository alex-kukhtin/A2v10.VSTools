// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace XamlEditor
{
	public class BooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is not Boolean boolVal)
				throw new InvalidCastException("Expected 'boolean'");
			if (parameter != null && parameter.ToString() == "Invert")
				return boolVal ? Visibility.Collapsed : Visibility.Visible;
			return boolVal ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
