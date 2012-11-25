using System;
using System.Collections.Generic;
using System.Html;
using System.Runtime.CompilerServices;
using System.Text;
using Lifelike.JScript.Admin.Managers;
using Lifelike.JScript.Admin.Modules.Console;

namespace Lifelike.JScript.Admin
{
	public delegate void Response();
	public delegate void Response<T>(T msg1);
	public delegate void Response<T, T1>(T msg1, T1 msg2);
	public delegate void Response<T, T1, T2>(T msg1, T1 msg2, T2 msg3);
	public delegate void Response<T, T1, T2, T3>(T msg1, T1 msg2, T2 msg3, T3 msg4);
	public delegate void ResponseParams<T>(T msg1, params object[] arr);
	public class Util
	{
		[InlineCode("debugger;")]
		public static dynamic Debugger()
		{

			return null;
		}

		public static ConsoleModule Console()
		{
			return PageManager.Context.ConsoleModule;
		}

		//[InlineCode("console")]
		//public static dynamic Console()
		//{

		//	return null;
		//}
		[InlineCode("console")]
		public static dynamic RealConsole()
		{
			return null;
		}
		public static string GradientGenerator(params Colour[] colours)
		{
			
			var linear = "linear-gradient(top";
			
			var webkit = " -webkit-gradient(linear, left bottom, left top";
			foreach (var c in colours)
			{
				linear = linear + ", "+ c.ToString() + c.Position + "% ";
				webkit = webkit + ", " + "color-stop(" + (c.Position == 0 ? 0f : ((float)c.Position) / 100) + ", " + c.ToString() + " ) ";
			}
			linear = linear + ")";
			webkit = webkit + ")";
			return  webkit;

		}
	}
	public class Colour
	{
		public int R { get; set; }
		public int G { get; set; }
		public int B { get; set; }
		public int A { get; set; }
		public int Position { get;set; }

		public override string ToString()
		{
			return string.Format("rgba({0},{1},{2},{3})", R, G, B, A);
		}
	}
}
