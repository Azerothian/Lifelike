using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using jQueryApi.UI.Widgets;
namespace Lifelike.JScript.Admin.Modules.Panels
{
	public class DockableControl : Control
	{
		private Label _header;

		public DockableControl(string name)
			: base(name)
		{
			_header = new Label("Header");
			_header.CssClass = "ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix";
			CssClass = "ui-dialog ui-widget ui-widget-content ui-corner-all";
			AddChild(_header);
		}

		public override void PreRender()
		{
			
		}
		public override void PostRender()
		{

			//jQueryApi.jQuery.FromElement(ControlContainer).
			base.PostRender();
		}
	}
}
