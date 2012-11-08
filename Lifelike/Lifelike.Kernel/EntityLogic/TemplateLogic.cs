using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.Fields;
using Lifelike.Kernel.Util;
using NHibernate;

namespace Lifelike.Kernel.EntityLogic
{
	public class TemplateLogic : LogicAbstract<Template>
	{
		public Template CreateTemplate(View view)
		{

			var _controlData = new Dictionary<string, IDictionary<PropertyInfo, Field>>();
			Page page = (Page)System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath("~/" + view.Layout.Path, typeof(Page));

			var r = page.GetType().BaseType;

			var layoutProps = Reflection.GetAllPropertiesByType<Field>(page);

			foreach (var m in (from v in view.Modules orderby v.Id select v))
			{
				Control c = page.LoadControl("~/" + m.Module.Path);
				var controlProps = Reflection.GetAllPropertiesByType<Field>(page);
				_controlData.Add(m.Module.Name, controlProps);
			}



			return null;
		}
	}
}
