using System;
using System.Collections.Generic;
using System.Text;
using jQueryApi.UI;
using jQueryApi;

using jQueryApi.UI.Widgets;
namespace Lifelike.JScript.Admin.Controls
{
	public class Label : Control
	{
		public Label(string name)
			: base(name) {

		}
		public string Text
		{
			get
			{
				return ControlContainer.InnerHTML;
			}
			set
			{
				ControlContainer.InnerHTML = value;
			}
		}


		public override void PreRender()
		{
			
		}

	}
}
