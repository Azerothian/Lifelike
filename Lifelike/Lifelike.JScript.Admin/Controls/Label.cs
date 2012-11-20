﻿using System;
using System.Collections.Generic;
using System.Text;
using jQueryApi.UI;
using jQueryApi;

using jQueryApi.UI.Widgets;
using System.Html;
namespace Lifelike.JScript.Admin.Controls
{
	public class Label : Control
	{
		public Label(string name)
			: base(name) {

		}
		public Label(string name, string tag)
			: base(name)
		{
			ControlContainer = Document.CreateElement(tag);
			Name = name;
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
