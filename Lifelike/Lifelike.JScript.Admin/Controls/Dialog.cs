using System;
using System.Collections.Generic;
using System.Text;
using jQueryApi.UI;
using jQueryApi;

using jQueryApi.UI.Widgets;
namespace Lifelike.JScript.Admin.Controls
{
	public class Dialog : Control
	{
		public bool IsCloseable { get; set; }
		public DialogOptions Options { get; set; }
		public Dialog(string name)
			: base(name) {
				Options = new DialogOptions()
				{

				};
		}
		public override void PreRender()
		{
			
		}
		public override void PostRender()
		{

			var dObject = jQuery.FromElement(ControlContainer).Dialog(Options);
			
			//Util.Debugger();
			if (!IsCloseable)
			{
				
				var p = jQuery.FromElement(ControlContainer).Parent();
				p.Find(".ui-dialog-titlebar > .ui-dialog-titlebar-close").Remove();
			}
			base.PostRender();
		}
	}
}
