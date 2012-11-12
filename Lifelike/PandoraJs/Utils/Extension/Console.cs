// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace PandoraJs.Utils.Extension
{
	[ScriptName("console"),IgnoreNamespace, Imported]
	public class Console
	{
		public static void Log(params object[] args) { }
		public static void Debug(params object[] args) { }
		public static void Info(params object[] args) { }
		public static void Warn(params object[] args) { }
		public static void Error(params object[] args) { }
		public static void LogStatement(string level, string statement) { }

	}
}
