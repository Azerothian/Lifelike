using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using jQueryApi.UI.Widgets;
using jQueryApi;
using Lifelike.JScript.Admin.Modules.Console;
using Lifelike.JScript.Admin.Modules.Panels;
using Lifelike.JScript.Admin.Managers;
namespace Lifelike.JScript.Admin.Modules.Log
{
	public class ConsoleModule : Control
	{
		Tabs tbViews;
		ConsoleView cvLog;
		ConsoleView cvDebug;
		DockableControl dlgWindow;
		public ConsoleModule(string name)
			: base(name)
		{

			//txtInput = new TextBox("txtInput");
			//btnSend = new Button("btnSend");
			dlgWindow = new DockableControl("dlgWindow");
			dlgWindow.Title = "Console";
			Util.RealConsole().log = new ResponseParams<string>(log);
			Util.RealConsole().debug = new ResponseParams<string>(debug);

			cvLog = new ConsoleView("cvLog");
			cvDebug = new ConsoleView("cvDebug");
			tbViews = new Tabs("tbViews");
			//tbViews.AddTab("Log", cvLog);
			//tbViews.AddTab("Debug", cvDebug);
			dlgWindow.AddChild(tbViews);
		}
		public override void PreRender()
		{
			CssClass = "consoleModule";

			//dlgWindow.Options = new DialogOptions()
			//{
			//	AutoOpen = true,
			//	CloseOnEscape = false,
			//	Height = 300,
			//	Width = 500,
			//	Title = "Console"
			//};
			AddChild(dlgWindow);
			dlgWindow.AddChild(tbViews);
			tbViews.AddTab("Log", cvLog);
			tbViews.AddTab("Debug", cvDebug);
			
			//txtInput.CssClass = "input";
			//btnSend.Text = "Send";
			
			//dlgWindow.AddChild(txtInput);
			//dlgWindow.AddChild(btnSend);
			
		}
		public override void PostRender()
		{
			PageManager.Context.panelLayout.pnlBottom.DropControl(dlgWindow);
			base.PostRender();
		}



		public void log(string message, params object[] arr)
		{
			cvLog.LogMessage(message, arr);
			
		}
		public void debug(string message, params object[] arr)
		{
			cvDebug.LogMessage(message, arr);
		}
	}
}
