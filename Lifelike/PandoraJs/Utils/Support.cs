// Support.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PandoraJs.Utils.Extension;

namespace PandoraJs.Utils
{
	public class Support
	{
		[InlineCodeAttribute("debugger;")]
		public static void DebuggerUtil()
		{
		}

		[InlineCodeAttribute("(console != undefined)")]
		public static bool IsConsoleSupported()
		{
			return false;
		}
		[InlineCodeAttribute("(type in document.createElement(element))")]
		public static bool ElementSupports(string element, string type)
		{
			return false;
		}

		[InlineCodeAttribute("(console != undefined) && (type in console)")]
		public static bool ConsoleSupports(string type)
		{
			return false;
		}


		public static bool IsTextBoxPlaceholderSupported { get { return ElementSupports("input", "placeholder"); } }
		public static bool IsConsoleWarnSupported { get { return ConsoleSupports("warn"); } }
		public static bool IsConsoleInfoSupported { get { return ConsoleSupports("info"); } }
		public static bool IsConsoleDebugSupported { get { return ConsoleSupports("debug"); } }
		public static bool IsConsoleErrorSupported { get { return ConsoleSupports("error"); } }
		public static bool IsConsoleLogSupported { get { return ConsoleSupports("log"); } }
	}
}
