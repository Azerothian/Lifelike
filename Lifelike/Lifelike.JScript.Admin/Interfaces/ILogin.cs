using System;
using System.Collections.Generic;
using System.Text;

namespace Lifelike.JScript.Admin.Interfaces
{
	public interface ILogin
	{
		string Username { get;  }
		string Password { get;  }
		bool Remember { get; }
	}
}
