using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using jQueryApi.UI.Widgets;
using jQueryApi;
using Lifelike.JScript.Admin.Modules.Console;
using Lifelike.JScript.Admin.Modules.Panels;
using Lifelike.JScript.Admin.Managers;
using System.Html;
namespace Lifelike.JScript.Admin.Modules.Console
{
	public class ConsoleModule : Control
	{
		Tabs tbViews;
		ConsoleView cvLog;
		ConsoleView cvDebug;
		ConsoleView cvSockets;
		DockableControl dockConsole;

		public ConsoleModule(string name)
			: base(name)
		{
			
		}
		public override void PreRender()
		{
			CssClass = "consoleModule";
			Log.log("[ConsoleModule] Creating Dockable");
			dockConsole = new DockableControl("dockConsole");
			dockConsole.Title = "Console";
			dockConsole.OnResize += dockConsole_OnResize;
			Log.log("[ConsoleModule] Creating Views");
			cvLog = new ConsoleView("cvLog");
			cvDebug = new ConsoleView("cvDebug");
			cvSockets = new ConsoleView("cvSockets");

			Log.log("[ConsoleModule] Creating Tabs");
			tbViews = new Tabs("tbViews");
			//tbViews.AddTab("Log", cvLog);
			//tbViews.AddTab("Debug", cvDebug);
			Log.log("[ConsoleModule] Adding Views and Tabs to Dockable");
			dockConsole.AddChild(tbViews);
			//dlgWindow.Options = new DialogOptions()
			//{
			//	AutoOpen = true,
			//	CloseOnEscape = false,
			//	Height = 300,
			//	Width = 500,
			//	Title = "Console"
			//};
			AddChild(dockConsole);
			dockConsole.AddChild(tbViews);
			tbViews.AddTab("Log", cvLog);
			tbViews.AddTab("Debug", cvDebug);
			tbViews.AddTab("Sockets", cvSockets);
			//txtInput.CssClass = "input";
			//btnSend.Text = "Send";
			
			//dlgWindow.AddChild(txtInput);
			//dlgWindow.AddChild(btnSend);
			Log.LogEvent += log;
			Log.DebugEvent += debug;
			Log.SocketEvent += socket;
			
		}


		void dockConsole_OnResize()
		{
			Log.log("Resize Dock", Height, Width, this);
			cvLog.Height = (int.Parse(dockConsole.Height) - 100) + "px";
			cvLog.Width = dockConsole.Width;

			cvDebug.Height = (int.Parse(dockConsole.Height) - 100) + "px";
			cvDebug.Width = dockConsole.Width;
			
			cvSockets.Height = (int.Parse(dockConsole.Height) - 100) + "px";
			cvSockets.Width = dockConsole.Width;
			//Resize();
		}

		public override void PostRender()
		{
			PageManager.Context.panelLayout.pnlBottom.DropControl(dockConsole);
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
		public void socket(string msg1, params object[] arr)
		{
			cvSockets.LogMessage(msg1, arr);
		}

	}
}
