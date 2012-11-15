using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Lifelike.Data.Entities.Xml;

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

		public static void SetProperties<T>(T o, List<Property> arr)
		{
			var t = o.GetType();
			var properties = from v in t.GetProperties()
							 let a = (from a in arr where a.Name == v.Name && a.Type == v.PropertyType.ToString() select a).FirstOrDefault()
							 where a != null
							 select new
							 {
								 Property = v,
								 Value = a
							 };
			foreach (var p in properties)
			{
				var type = Type.GetType(p.Value.Type);
				var val = p.Value.Value;
				object cv = Convert.ChangeType(val, type);
				p.Property.SetValue(o, cv, null);
			}


		}
	}
}
