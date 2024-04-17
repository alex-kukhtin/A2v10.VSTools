// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;
using System.Windows.Markup;

namespace XamlEditor
{
	public class EnumBindingSource : MarkupExtension
	{
		public Type EnumType;

		public EnumBindingSource() { }

		public EnumBindingSource(Type enumType)
		{
			EnumType = enumType;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (EnumType == null)
				throw new InvalidOperationException("The EnumType must be specified.");

			return Enum.GetValues(EnumType);
		}
	}
}
