using System;
using System.Collections.Generic;
using System.Html;
using System.Text;

namespace Lifelike.JScript.Admin.Controls
{
	public class ListItem : Control
	{
		public ListItem(string name)
			: base(name)
		{
			ControlContainer = Document.CreateElement("li");
			Name = name;
		}
		public override void PreRender()
		{

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

	}
}
