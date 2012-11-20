using System;
using System.Collections.Generic;
using System.Text;
using jQueryApi;
using jQueryApi.UI.Widgets;

namespace Lifelike.JScript.Admin.Controls
{
	public class Tabs : Control
	{

		List lstTabs;
		public Tabs(string name)
			: base(name)
		{
			lstTabs = new List("lstTabs");
			AddChild(lstTabs);
		}
		public void AddTab(string display, Control control)
		{
			AddChild(control);
			
			var p = control.Parent;
			var clientid = control.ClientId;
			display = "<a href='#" + clientid + "'>" + display + "</a>";

			lstTabs.AddChild(new ListItem(display) { Text = display });
		}


		public override void PreRender()
		{
		}
		public override void PostRender()
		{

			jQuery.FromElement(ControlContainer).Tabs();
			base.PostRender();
		}
	}
}
