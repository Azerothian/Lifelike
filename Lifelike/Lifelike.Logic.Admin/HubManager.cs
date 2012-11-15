using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using SignalR;
using SignalR.Client.Hubs;
using SignalR.Hosting.AspNet.Infrastructure;
using SignalR.Hosting.AspNet.Routing;
using SignalR.Hosting.Self;
using SignalR.Hubs;

namespace Lifelike.Logic.Admin
{
	public class HubManager
	{

		public void Initialise()
		{
			var resolver = new DefaultDependencyResolver();
			var routes = RouteTable.Routes;
			var url = "~/signalr";
			//resolver.InitializePerformanceCounters(GetInstanceName(), AspNetHandler.AppDomainTokenSource.Token);

			var existing = routes["signalr.hubs"];
			if (existing != null)
			{
				routes.Remove(existing);
			}

			string routeUrl = url;
			if (!routeUrl.EndsWith("/"))
			{
				routeUrl += "/{*operation}";
			}

			routeUrl = routeUrl.TrimStart('~').TrimStart('/');

			var locator = new Lazy<IAssemblyLocator>(() => new AssemblyLocator());
			resolver.Register(typeof(IAssemblyLocator), () => locator.Value);

			var route = new Route(routeUrl, new HubDispatcherRouteHandler(url, resolver));
			route.Constraints = new RouteValueDictionary();
			route.Constraints.Add("Incoming", new IncomingOnlyRouteConstraint());
			route.Constraints.Add("IgnoreJs", new IgnoreJsRouteConstraint());
			routes.Add("signalr.hubs", route);
		//	return route;

		//	string url = "http://cms.illisian.com.au:15000/";
		//	var server = new Server(url);

		//	// Map the default hub url (/signalr)
		//	server.MapHubs();

		//	// Start the server
		//	server.Start();

		////	Console.WriteLine("Server running on {0}", url);


		//	Thread _thread = new Thread(Worker);
		//	_thread.Start();
		//	// Keep going until somebody hits 'x'
			
		//	var hubConnection = new HubConnection("http://localhost/mysite");

		//	// Create a proxy to the chat service
		//	var chat = hubConnection.CreateHubProxy("chat");

		//	// Print the message when it comes in
		//	chat.On("addMessage", message => Console.WriteLine(message));

		//	// Start the connection
		//	hubConnection.Start().Wait();
		}
		public bool Running { get; set; }
		public void Worker()
		{
			do
			{

				Thread.Sleep(100);

			} while (Running);
		}

	}

	public class AssemblyLocator : DefaultAssemblyLocator
	{
		public override IEnumerable<Assembly> GetAssemblies()
		{
			return new [] { Assembly.GetAssembly(typeof(AssemblyLocator)) } ;
		}
	}
	public class DRes : IDependencyResolver
	{
		public object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			throw new NotImplementedException();
		}

		public void Register(Type serviceType, IEnumerable<Func<object>> activators)
		{
			throw new NotImplementedException();
		}

		public void Register(Type serviceType, Func<object> activator)
		{
			throw new NotImplementedException();
		}
	}
}
