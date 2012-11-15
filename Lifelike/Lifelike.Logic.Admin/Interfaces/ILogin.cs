using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Logic.Admin.Interfaces
{
	public interface ILogin
	{
		string Username { get;  }
		string Password { get; }
		bool Remember { get; }
	}
}
