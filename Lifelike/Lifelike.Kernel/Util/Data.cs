using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Lifelike.Kernel.Util
{
	public class Data
	{
		private static Dictionary<string, object> _offlineStorage = new Dictionary<string, object>();
		public static void SaveField(string name, object data)
		{
			if (HttpContext.Current != null)
			{
				HttpContext.Current.Session[name] = data;
			}
			else
			{
				if (_offlineStorage.Keys.Contains(name))
				{
					_offlineStorage[name] = data;
				}
				else
				{
					_offlineStorage.Add(name, data);
				}
			}
		}
		public static T LoadField<T>(string name)
		{
			if (HttpContext.Current != null)
			{
				return (T)HttpContext.Current.Session[name];
			}
			else
			{
				if (_offlineStorage.Keys.Contains(name))
				{
					return (T)_offlineStorage[name];
				}
			}
			return default(T);
		}
	}
}
