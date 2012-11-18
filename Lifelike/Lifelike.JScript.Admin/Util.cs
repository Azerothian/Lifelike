using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lifelike.JScript.Admin
{

	public delegate void Response<T>(T msg1);
	public delegate void Response<T, T1>(T msg1, T1 msg2);
	public delegate void Response<T, T1, T2>(T msg1, T1 msg2, T2 msg3);
	public class Util
	{
		[InlineCode("debugger;")]
		public static dynamic Debugger()
		{

			return null;
		}
		[InlineCode("console")]
		public static dynamic Console()
		{

			return null;
		}
	}
}
