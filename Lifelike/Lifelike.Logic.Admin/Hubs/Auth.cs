using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.Hubs;

namespace Lifelike.Logic.Admin.Hubs
{
	public class Auth : Hub, IDisconnect, IConnected
	{
		public Task login(string username, string password, bool remember)
		{

			return Caller.loginResponse(true);
		}

		public System.Threading.Tasks.Task Connect()
		{
			return Clients.joined(Context.ConnectionId, DateTime.Now.ToString());
		}

		public System.Threading.Tasks.Task Reconnect(IEnumerable<string> groups)
		{
			return Clients.rejoined(Context.ConnectionId, DateTime.Now.ToString());
		}

		public System.Threading.Tasks.Task Disconnect()
		{
			return Clients.leave(Context.ConnectionId, DateTime.Now.ToString());
		}
	}
}
