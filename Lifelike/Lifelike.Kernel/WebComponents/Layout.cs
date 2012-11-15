using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Lifelike.Data.Entities.Xml;
using Lifelike.Kernel.EntityLogic;
using Lifelike.Kernel.Util;

namespace Lifelike.Kernel.WebComponents
{
	public class Layout : System.Web.UI.Page
	{
		public List<Property> __templateData { get; set; }
	}
}
