using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.Entities.Xml;
using Lifelike.Kernel.Util;
using NHibernate;

namespace Lifelike.Kernel.EntityLogic
{
	public class ModuleLogic : LogicAbstract<Module>
	{

		public static Lifelike.Kernel.WebComponents.Module[] GetAllWebCtlModulesFromItemByCurrentView(Page page, Item item)
		{
			var _lstModules = new List<WebComponents.Module>();
			var data = TemplateLogic.LoadFromItem(item);
			foreach (var m in ViewLogic.GetCurrentView(item).Modules)
			{
				var properties = (from v in data.PropertyGroups where v.Name == m.Module.Name select v).FirstOrDefault();
				_lstModules.Add(LoadModule(page, m.Module, properties.Properties));
			}
			return _lstModules.ToArray();


		}
		public static Lifelike.Kernel.WebComponents.Module LoadModule(Page page, Lifelike.Kernel.Entities.Module module, List<Property> data = null)
		{

			var c = (Lifelike.Kernel.WebComponents.Module)page.LoadControl("~/" + module.Path);
			c.__templateData = data;
			if (data != null)
			{
				Reflection.SetProperties(c, data);
			}
			return c;
		}



	}
}
