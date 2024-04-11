// Copyright © 2024 Oleksandr Kukhtin. All rights reserved.

using System;

namespace XamlEditor
{
	internal static class StringExtensions
	{
		public static String Singular(this String s) 
		{
			if (s.EndsWith("ies"))
				return s.Substring(0, s.Length - 3) + "y";
			else if (s.EndsWith("s"))
				return s.Substring(0, s.Length - 1);	
			return s;
		}
	}
}
