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

		public override void PreRender()
		{

		}
		public override void PostRender()
		{

			lblMessages.CssClass = "messages";
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

			lblMessages.Text = lblMessages.Text + "<br/>" + message;
		}
	}
}
