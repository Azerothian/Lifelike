using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lifelike.Kernel.Fields;
using Lifelike.Kernel.WebComponents;

namespace Lifelike.WebAdmin.files.layouts
{
	public partial class Main : Layout
	{
		[Field]
		public string TestLayoutMessage { get { return lblMessage.Text; } set { lblMessage.Text = value; } }
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}