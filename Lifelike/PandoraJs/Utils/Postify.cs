// Postify.cs
//

using System;
using System.Collections.Generic;
using System.Collections;

namespace PandoraJs.Utils
{
	/// <summary>
	/// 
	/// </summary>
	public class Postify
	{
		int _recursionLimit;
		int _recursionCount = 0;
		object _result = null;

		public object Parse(object obj, int recursionLimit)
		{
			_recursionLimit = recursionLimit;
			_result = new object();
			BuildResult(obj, "");
			return _result;
		}


		private void BuildResult(object objt, string prefix)
		{
			_recursionCount++;
			if (_recursionLimit < _recursionCount)
				return;
			foreach (var entry in (Dictionary<string, string>)objt)
			{
				if (Type.HasField(objt, entry.Key) || Type.HasProperty(objt, entry.Key.Replace("set_", "")))
				{
					string postKey = Number.IsFinite(Number.Parse(entry.Key))
					? (prefix != "" ? prefix : "") + "[" + entry.Key + "]"
					: (prefix != "" ? prefix + "." : "") + entry.Key;
					Type type = entry.Value.GetType();
					//Logging.Debug("Type of entry is " + type, new object[] { entry });
					if (type == typeof(string) || type == typeof(bool) || type == typeof(Number))
					{
						Type.SetField(_result, entry.Key, entry.Value);
					}
					else if (type == typeof(Date))
					{
						Type.SetField(_result, entry.Key, (entry.Value as Date).ToUTCString().Replace("UTC", "GMT"));
					}
					else
					{
						BuildResult(entry.Value, postKey != "" ? postKey : entry.Key);
					}
				}
			}

		}

		/// <summary>
		/// Creates a base object from all the properties defined in a ScriptSharp Object.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		public object CreatePropertyObject(Dictionary<string,string> target)
		{
			object ret = new object();
			foreach (string key in target.Keys)
			{

				object o = target[key];
				object neu = new object();

				List<string> validProperties = new List<string>();

				foreach (var entry in target)
				{
					if (entry.Key.StartsWith("set_"))
					{
						string prop = entry.Key.Remove(0, 4);
						if (Type.HasProperty(o, prop))
						{
							validProperties.Add(prop);
						}
					}
				}

				foreach (string s in validProperties)
				{
					Type.SetField(neu, s, Type.GetProperty(o, s));
				}
				Type.SetField(ret, key, neu);

			}
			return ret;
		}
	

	}
}
