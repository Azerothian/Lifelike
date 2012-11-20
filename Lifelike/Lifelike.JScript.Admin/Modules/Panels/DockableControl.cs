using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using jQueryApi.UI.Widgets;
using jQueryApi.UI.Interactions;
using jQueryApi.UI.Effects;
using Lifelike.JScript.Admin.Modules.Chat;
namespace Lifelike.JScript.Admin.Modules.Panels
{
	public class DockableControl : Control
	{
		public string Title { get { return _header.Text; } set { _header.Text = value; } }
		private BaseControl _headerContainer;
		private Label _header;
		public DraggableOptions draggableOptions { get; set; }
		//
		public DockableControl(string name)
			: base(name)
		{
			_header = new Label("Header");
			_headerContainer = new BaseControl("HeaderContainer");

			_headerContainer.CssClass = "ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix";
			_header.CssClass = "ui-dialog-title";
			CssClass = "ui-dialog ui-widget ui-widget-content ui-corner-all";

			draggableOptions = new DraggableOptions()
			{
				Handle = _headerContainer.ControlContainer,
				ZIndex = 10,
				Scope = "draggable"
			};


			_headerContainer.AddChild(_header);
			AddChild(_headerContainer);
		}


		public void DockableControl_Draggable_OnCreate(jQueryApi.jQueryEvent e)
		{

		}

		public override void PreRender()
		{
			
		}
		public override void PostRender()
		{

			jQueryApi.jQuery.FromElement(ControlContainer).Draggable(draggableOptions);
			base.PostRender();
		}
	}
}
