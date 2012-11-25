using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Modules.Panels;

namespace Lifelike.JScript.Admin.Modules.Dock
{
	public class StartMenu : Control
	{
		public Panel _menu;
		public StartMenu(string name)
			: base(name)
		{

		}


		public void startmenu_Click(jQueryApi.jQueryEvent e)
		{
			
			_menu.Visible = !_menu.Visible;
		}

		public override void PreRender()
		{
			CssClass = "startmenu";
			Height = "40px";
			Width = "40px";
			Margin = "5px";
			Background = "#1d74bb";
			Float = "left";

			OnClick(startmenu_Click);
			_menu = new Panel("Menu");

			_menu.Float = "left";
			_menu.CssClass = _menu.CssClass + " menu";
			_menu.Top = "50px";
			_menu.Height = (Window.InnerHeight - 50) + "px";
			_menu.Width = Window.InnerWidth + "px";
			_menu.Visible = false;
			_menu.Background = "#16499a";
			_menu.zIndex = "9999";
			Parent.AddChild(_menu);
		}
	}
}
