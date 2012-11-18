using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Lifelike.Logic.Admin;
using Microsoft.AspNet.SignalR.Hosting.AspNet.Routing;
using Microsoft.AspNet.SignalR.Hosting.Common;
using Microsoft.AspNet.SignalR.Hosting.AspNet;
using Microsoft.AspNet.SignalR.Hosting;
using SignalR;
using Lifelike.Logic.Admin.Hubs;
namespace Lifelike.WebAdmin
{
	public class Global : System.Web.HttpApplication
	{
		HubManager _hubManager;
		protected void Application_Start(object sender, EventArgs e)
		{
			Lifelike.Kernel.Context.Initialise();
			_hubManager = new HubManager();
			_hubManager.Initialise();
			//RouteTable.Routes.Ma
			
			//RouteConfig.
			
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{
			_hubManager.Running = false;
		}
	}
}