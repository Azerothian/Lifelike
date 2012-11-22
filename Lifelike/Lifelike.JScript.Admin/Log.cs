using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lifelike.JScript.Admin
{
	public class Log
	{
		public static event ResponseParams<string> LogEvent;
		public static event ResponseParams<string> DebugEvent;
		public static event ResponseParams<string> SocketEvent;
		//static ResponseParams<string> oldLog;
		//static ResponseParams<string> oldDebug;

		public Log()
		{
			//Log.log("[ConsoleModule] Routing log and debug.");
		
		}

		private static  void RouteLogs()
		{
			//if (oldLog == null)
			//{
			//	oldLog = routeLog();
			//	oldDebug = routeDebug();
			//	Util.RealConsole().log = new ResponseParams<string>(log);
			//	Util.RealConsole().debug = new ResponseParams<string>(debug);
			//}
		}
		[InlineCode("console.log")]
		private static ResponseParams<string> routeLog()
		{
			return null;
		}
		[InlineCode("console.debug")]
		private static ResponseParams<string> routeDebug()
		{
			return null;
		}
		public static void log(string message, params object[] objects)
		{
			Util.RealConsole().log(message, objects);
			//TODO: get browser logs working as well
			//RouteLogs();
			//if (oldLog != null)
			//	oldLog(message, objects);
			if (LogEvent != null)
			{
				LogEvent(message, objects);
			}
		}
		public static void debug(string message, params object[] objects)
		{
			Util.RealConsole().debug(message, objects);

			//RouteLogs();
			//if (oldDebug != null)
			//	oldDebug(message, objects);
			if (DebugEvent != null)
			{
				DebugEvent(message, objects);
			}
		}


		internal static void sockets(string message, params object[] objects)
		{
			var m = "[SOCKET]" + message;
			Util.RealConsole().log(m, objects);

			if (SocketEvent != null)
			{
				SocketEvent(message, objects);
			}
		}
	}
}
