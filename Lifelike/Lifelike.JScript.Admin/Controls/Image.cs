using System;
using System.Collections.Generic;
using System.Html;
using System.Text;

namespace Lifelike.JScript.Admin.Controls
{
	public class Image : Control
	{
		public string Src { get { return (string)ControlContainer.GetAttribute("src"); } set { ControlContainer.SetAttribute("src", value); } }
		public Image(string name)
			: base(name)
		{
			ControlContainer = Document.CreateElement("img");
		}

		public override void PreRender()
		{
			
		}
	}
}
