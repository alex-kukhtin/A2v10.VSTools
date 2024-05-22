// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace XamlEditor;

public class StringToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var strVal = value?.ToString();
		if (parameter != null && parameter.ToString() == "Invert")
			return String.IsNullOrEmpty(strVal) ? Visibility.Collapsed : Visibility.Visible;
		return !String.IsNullOrEmpty(strVal) ? Visibility.Visible : Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
