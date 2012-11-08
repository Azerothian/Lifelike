using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lifelike.Kernel.Fields;
using Lifelike.WebComponents;

namespace Lifelike.WebAdmin.files.modules
{
	public partial class TestModule : Module
	{
		[Field]
		public string TestModuleMessage { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}