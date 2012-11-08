using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lifelike.Kernel.Fields;

namespace Lifelike.WebAdmin.files.layouts
{
	public partial class Main : System.Web.UI.Page
	{
		[Field]
		public string TestLayoutMessage { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}