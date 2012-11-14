using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalR.Hubs;

namespace Lifelike.Logic.Admin.Hubs
{
	public class Chat : Hub, IDisconnect, IConnected
	{
		public Task SendMessage(string message)
		{
			// Call the addMessage method on all clients            
			//Clients.All.addMessage(message);
			return Clients.addMessage(message);
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
