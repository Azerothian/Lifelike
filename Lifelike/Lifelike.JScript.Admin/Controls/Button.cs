using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using jQueryApi;
using jQueryApi.UI.Widgets;

namespace Lifelike.JScript.Admin.Controls
{
	public class Button : Control
	{
		public jQueryEventHandler OnClick;
		Element _element;
		public Button(string name)
			: base(name) {
				_element = Document.CreateElement("a");
				ControlContainer.AppendChild(_element);
		}


		public override void PreRender()
		{
		}
		public override void PostRender()
		{
			if (OnClick != null)
			{
				jQuery.FromElement(_element).Click(OnClick);
			}
			jQuery.FromElement(_element).Button();
			base.PostRender();
		}



		public string Text { get { return _element.InnerHTML; } set { _element.InnerHTML = value; } }
	}
}
