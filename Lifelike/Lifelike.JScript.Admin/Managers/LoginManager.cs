using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Interfaces;

namespace Lifelike.JScript.Admin.Managers
{

	public class LoginManager
	{
		public Response<bool> LoginResponseEvent;
		ILogin _inf;
		public LoginManager(ILogin login)
		{
			_inf = login;
			
		}

		internal void LoginUser()
		{
			HubManager.Context.GetConnection().auth.server.login(_inf.Username, _inf.Password, _inf.Remember);
		}
	}
}
