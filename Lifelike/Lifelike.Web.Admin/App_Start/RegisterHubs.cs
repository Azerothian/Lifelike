using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hosting.AspNet;

[assembly: PreApplicationStartMethod(typeof(Lifelike.Web.Admin.RegisterHubs), "Start")]

namespace Lifelike.Web.Admin
{
    public static class RegisterHubs
    {
        public static void Start()
        {
            // Register the default hubs route: ~/signalr/hubs
            RouteTable.Routes.MapHubs();            
        }
    }
}
