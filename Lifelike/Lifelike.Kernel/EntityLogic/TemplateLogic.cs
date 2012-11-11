using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.Entities.Xml;
using Lifelike.Kernel.Fields;
using Lifelike.Kernel.Util;

namespace Lifelike.Kernel.EntityLogic
{
	public static class TemplateLogic
	{

		public static TemplateData LoadFromItem(Item item)
		{
			if (!string.IsNullOrEmpty(item.Value))
			{
				return Util.Serialisation.Xml.Generics.DeserializeObjectFromString<TemplateData>(item.Value);
			}
			return null;
		}

		private static List<Property> CreatePropertyList(IDictionary<PropertyInfo, Field> properties, List<Property> data = null)
		{
			List<Property> list = data;
			if (list == null)
			{
				list = new List<Property>();
			}
			
			foreach (var v in properties)
			{
				var prop = (from f in list where f.Name == v.Key.Name select f).FirstOrDefault();
				if (prop == null)
				{
					list.Add(new Property()
					{
						Name = v.Key.Name,
						Type = v.Key.PropertyType.ToString(),
						Value = ""
					});
				}
				else
				{
					list.Remove(prop);
					prop.Name = v.Key.Name;
					prop.Type = v.Key.PropertyType.ToString();
					list.Add(prop);
				}
			}
			return list;
		}

		public static string CreateTemplateFromView(View view, TemplateData template = null)
		{

			//var _controlData = new Dictionary<string, IDictionary<PropertyInfo, Field>>();
			Page page = (Page)System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath("~/" + view.Layout.Path, typeof(Page));

			var r = page.GetType().BaseType;
			//var 

			if (template == null)
			{
				template = new TemplateData();
			}
			List<Property> lstMainProps = null;
			if (template.Properties == null)
			{
				lstMainProps = new List<Property>();
			}
			else
			{
				lstMainProps= template.Properties.ToList();
			}

			var layoutProps = Reflection.GetAllPropertiesByType<Field>(page);

			template.Properties = CreatePropertyList(layoutProps, template.Properties);

			var lstPropGroup = new List<PropertyGroup>();
			foreach (var m in (from v in view.Modules orderby v.Id select v))
			{

				lstPropGroup.Add(CreatePropertyGroupFromModule(page, m.Module));
			}
			template.PropertyGroups = lstPropGroup;

			return Util.Serialisation.Xml.SerializeObject(template); ;
		}

		public static PropertyGroup CreatePropertyGroupFromModule(Page page, Lifelike.Kernel.Entities.Module m)
		{
			var propGroup = new PropertyGroup();
			var lstCtlProps = new List<Property>();

			Control c = page.LoadControl("~/" + m.Path);


			propGroup.Name = m.Name;
			propGroup.Type = c.GetType().ToString();

			var controlProps = Reflection.GetAllPropertiesByType<Field>(c);
			propGroup.Properties = CreatePropertyList(controlProps, propGroup.Properties);
			return propGroup;
			

		}


	}
}
