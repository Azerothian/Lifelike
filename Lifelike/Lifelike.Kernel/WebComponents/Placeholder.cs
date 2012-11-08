﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lifelike.WebComponents
{
	public class Placeholder : WebControl
	{
		Panel panel = new Panel();
		public Placeholder()
		{
			this.Controls.Add(panel);
		}
		public void AddControl(Control c)
		{
			panel.Controls.Add(c);
		}
		public void AddControlAt(Control c, int index)
		{
			panel.Controls.Add(c);
		}

	}
}