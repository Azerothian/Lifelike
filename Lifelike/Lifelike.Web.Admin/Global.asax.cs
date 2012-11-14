using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Lifelike.Logic.Admin;
using SignalR.Hosting.AspNet.Routing;
using SignalR.Hosting.Common;
using SignalR.Hosting.AspNet;
using SignalR;
using Lifelike.Logic.Admin.Hubs;
namespace Lifelike.WebAdmin
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			Lifelike.Kernel.Context.Initialise();
			HubManager _hubManager = new HubManager();
			_hubManager.Initialise();
			RouteTable.Routes.MapHubs();			//RouteTable.Routes.
			
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

		}
	}
}