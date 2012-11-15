using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Logic.Admin.Interfaces;

namespace Lifelike.Logic.Admin
{
	public class AuthManager
	{
		public ILogin _infLogin;

		public AuthManager(ILogin inf)
		{
			_infLogin = inf;
		}

		public void Login()
		{

		}
	}
}
