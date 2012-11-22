using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;

namespace Lifelike.JScript.Admin.Modules.Console
{
	public class ConsoleView : Control
	{
		Label lblMessages;
		public ConsoleView(string name)
			: base(name)
		{
			lblMessages = new Label("lblMessages");
			AddChild(lblMessages);
			

		}

		void ConsoleView_OnResize()
		{
			Log.log("Resize View", Height, Width, this);
			lblMessages.Height = Height;
			lblMessages.Width = "100%";
			lblMessages.CssClass = "vertical-scroll";
		}

		public override void PreRender()
		{
			OnResize += ConsoleView_OnResize;
		}
		public override void PostRender()
		{
			ConsoleView_OnResize();
			base.PostRender();
		}

		public void LogMessage(string message, params object[] arr)
		{
			if (arr != null)
			{
				var r = "";
				foreach (var o in arr)
				{
					if (o is String)
					{
						r = r + " " + (string)o;
					}
				}
				message = message + r;
			}
			if (!string.IsNullOrEmpty(lblMessages.Text))
			{
				lblMessages.Text = lblMessages.Text + "<br/>";
			}

			lblMessages.Text = lblMessages.Text + message;
			lblMessages.ScrollDown();
		}
	}
}
