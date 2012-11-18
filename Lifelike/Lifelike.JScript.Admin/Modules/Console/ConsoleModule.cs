using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using jQueryApi.UI.Widgets;
using jQueryApi;
namespace Lifelike.JScript.Admin.Modules.Log
{
	public class ConsoleModule : Control
	{
		Label lblMessages;
		TextBox txtInput;
		Button btnSend;
		Dialog dlgWindow;
		public ConsoleModule(string name)
			: base(name)
		{
						lblMessages = new Label("lblMessages");
			txtInput = new TextBox("txtInput");
			btnSend = new Button("btnSend");
			dlgWindow = new Dialog("dlgWindow");
		}
		public override void PreRender()
		{
			CssClass = "consoleModule";

			dlgWindow.Options = new DialogOptions()
			{
				AutoOpen = true,
				CloseOnEscape = false,
				Height = 300,
				Width = 500,
				Title = "Console"
			};
			lblMessages.CssClass = "messages";
			txtInput.CssClass = "input";
			btnSend.Text = "Send";
			dlgWindow.AddChild(lblMessages);
			dlgWindow.AddChild(txtInput);
			dlgWindow.AddChild(btnSend);
			AddChild(dlgWindow);
		}
		public override void PostRender()
		{			
			base.PostRender();
		}

		public void LogMessage(string message)
		{
			lblMessages.Text = lblMessages.Text + "<br/>" + message;
		}



	}
}
