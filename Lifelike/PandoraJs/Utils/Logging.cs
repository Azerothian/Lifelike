using System;
using System.Collections.Generic;
using jQueryApi;


namespace PandoraJs.Utils
{
	public delegate void LogEventHandler(LoggingType logType, string message, object[] items);

	public enum LoggingType
	{
		Debug = 1,
		Info = 2,
		Warning = 3,
		Error = 4
	}

	public static class Logging
	{
		static RealDictionary _logEvents;
		public static void AddLogEvent(LoggingType logType, LogEventHandler e)
		{
			if (_logEvents == null)
			{
				_logEvents = new RealDictionary();
			}
			if (!_logEvents.ContainsKey(logType))
			{
				_logEvents.Add(logType, new List<LogEventHandler>());
			}
			((List<LogEventHandler>)_logEvents[logType]).Add(e);
		}
		public static void Log(LoggingType logType, string message, object[] args)
		{
			if (_logEvents != null)
			{
				if (_logEvents.ContainsKey(logType))
				{
					foreach (LogEventHandler e in ((List<LogEventHandler>)_logEvents[logType]))
					{
						e.Invoke(logType, message, args);
					}
				}
			}
		}
		public static void Info(string message, object[] args)
		{
			Log(LoggingType.Info, message, args);
		}
		public static void Warn(string message, object[] args)
		{
			Log(LoggingType.Warning, message, args);
		}
		public static void Debug(string message, object[] args)
		{
			Log(LoggingType.Debug, message, args);
		}
		public static void Error(string message, object[] args)
		{
			Log(LoggingType.Error, message, args);
		}
	}
}
