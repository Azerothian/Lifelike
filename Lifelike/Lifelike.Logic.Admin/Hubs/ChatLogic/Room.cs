using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike.Logic.Admin.Hubs.ChatLogic
{
	public class Room
	{
		public string Name { get; set; }
		public List<User> Users { get; set; }
	}
}
