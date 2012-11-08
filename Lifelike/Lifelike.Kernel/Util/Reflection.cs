using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lifelike.Kernel.Util
{
	public class Reflection
	{

		public static IDictionary<PropertyInfo, T> GetAllPropertiesByType<T>(object o)
		{
			var t = o.GetType();
			var properties = from v in t.GetProperties()
							 let rr = v.GetCustomAttributes(typeof(T), false).FirstOrDefault()
							 where rr != null
							 select new
							 {
								 Property = v,
								 Param = (T)rr
							 };

			var dict = new Dictionary<PropertyInfo, T>();
			foreach (var v in properties)
			{
				dict.Add(v.Property, v.Param);
			}
			return dict;
		}
	}
}
