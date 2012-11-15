using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Lifelike.Data.Entities.Xml;
using Lifelike.Kernel.Util;

namespace Lifelike.Kernel.WebComponents
{
	public class Module : UserControl
	{
		public List<Property> __templateData { get; set; }
		public Module()
		{
			this.Init += Module_Init;
		}


		void Module_Init(object sender, EventArgs e)
		{
			if (__templateData != null)
			{
				Reflection.SetProperties(this, __templateData);
			}
		}
	}
}
