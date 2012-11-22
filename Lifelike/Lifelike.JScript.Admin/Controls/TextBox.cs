using System;
using System.Collections;
using System.Collections.Generic;
using System.Html;
using System.Text;

namespace Lifelike.JScript.Admin.Controls
{
	public class TextBox : Control
	{
		public TextBox(string name) : base (name) {
	
			_element = (InputElement)Document.CreateElement("input");
			OnResize += TextBox_OnResize;
			Width = "250px";
		}

		void TextBox_OnResize()
		{
			jQueryApi.jQuery.FromElement(_element).Width(Width);
		}

		InputElement _element;
		public jQueryApi.jQueryEventHandler OnEnter;
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
		public override void PostRender()
		{
			jQueryApi.jQuery.FromElement(_element).Keypress(keyCheck);


			base.PostRender();
		}

		private void keyCheck(jQueryApi.jQueryEvent e)
		{
			if (e.Which == 13 && OnEnter != null)
			{
				OnEnter(e);
			}
		}

	}
}
