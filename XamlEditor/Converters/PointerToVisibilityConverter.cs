// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace XamlEditor;

public class PointerToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value != null ? Visibility.Visible : Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
