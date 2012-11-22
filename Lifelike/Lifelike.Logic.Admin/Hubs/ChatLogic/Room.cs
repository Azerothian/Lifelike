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
		public List<String> Users { get; set; }

		//internal bool IsInGroup(string Name)
		//{
		//	if (Users != null)
		//	{
		//		foreach (var u in Users)
		//		{
		//			if (u.Username == Name)
		//				return true;
		//		}
		//	}
		//	return false;
		//}

		internal string GetUser(string Name)
		{
			if (Users != null)
			{
				foreach (var u in Users)
				{
					if (u == Name)
						return u;
				}
			}
			return null;
		}
	}
}
