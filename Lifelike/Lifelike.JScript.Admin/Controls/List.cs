using System;
using System.Collections.Generic;
using System.Html;
using System.Text;

namespace Lifelike.JScript.Admin.Controls
{
	public class List : Control
	{
		public List(string name)
			: base(name)
		{
			ControlContainer = Document.CreateElement("ul");
			Name = name;
		}
		public override void PreRender()
		{

		}

	}
}
