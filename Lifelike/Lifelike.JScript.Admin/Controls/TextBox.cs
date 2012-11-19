using System;
using System.Collections.Generic;
using System.Html;
using System.Text;

namespace Lifelike.JScript.Admin.Controls
{
	public class TextBox : Control
	{
		public TextBox(string name) : base (name) {
			_element = (InputElement)Document.CreateElement("input");
		}

		InputElement _element;
		public string Value
		{
			get
			{
				return _element.Value;
			}
			set
			{
				_element.Value = null;
			}
		}

		public string Placeholder
		{
			get
			{
				return (string)_element.GetAttribute("placeholder");
			}
			set
			{
				_element.SetAttribute("placeholder", value);
			}
		}
		public override void PreRender()
		{

			_element.SetAttribute("type", "text");
			if(!String.IsNullOrEmpty(CssClass))
			{

				if (CssClass.IndexOf("textbox") > -1)
				{
					CssClass = CssClass + " textbox";
				}
			}
			ControlContainer.AppendChild(_element);
		}

	}
}
