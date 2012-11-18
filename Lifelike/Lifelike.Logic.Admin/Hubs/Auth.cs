using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;


namespace Lifelike.Logic.Admin.Hubs
{
	public class Auth : Hub
	{
		public void login(string username, string password, bool remember)
		{

			Clients.Caller.loginResponse(username, true);
		}

	}
}
