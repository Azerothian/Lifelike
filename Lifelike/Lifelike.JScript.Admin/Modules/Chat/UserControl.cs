using System;
using System.Collections.Generic;
using System.Text;
using jQueryApi.UI.Widgets;
namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class UserControl : Control
	{
		private string room;
		public UserControl(string name, string room): base(name)
		{
			Height = "25";
			Width = "100%";
			Background = "#647687";
			this.room = room;

		}


		public override void PreRender()
		{
			
		}
	}
	public class UserItemControl : Control
	{


		public string Name { get; set; }
		public UserItemControl(string name)
			: base(name)
		{
			Name = name;
			Height = "17";
			Width = "17";
			Background = "black";
			Float = "left";
			Margin = "3px";
			Title = name;
		}


		public override void PreRender()
		{

		}

		public override void PostRender()
		{
			jQueryApi.jQuery.FromElement(ControlContainer).Tooltip();
		}
	}
}
