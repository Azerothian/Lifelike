using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lifelike.Logic.Admin.Hubs.ChatLogic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Lifelike.Logic.Admin.Hubs
{
	public class Chat : Hub
	{
		public static Dictionary<string, string> Usernames { get; set; }
		public static Dictionary<string, Room> Rooms { get; set; }

		public string GetConnectionId { get { return this.Context.ConnectionId; } }

		public string Name
		{
			get
			{
				if (Usernames == null)
					Usernames = new Dictionary<string, string>();
				if (Usernames.ContainsKey(GetConnectionId))
				{
					return Usernames[GetConnectionId];
				}
				return "";

			}
			set
			{
				if (Usernames == null)
					Usernames = new Dictionary<string, string>();
				if (Usernames.ContainsKey(GetConnectionId))
				{
					Usernames[GetConnectionId] = value;
					return;
				}
				Usernames.Add(GetConnectionId, value);

			}
		}

		public void registerName(string name)
		{
			Name = name;
            //joinRoom(name);
			Clients.Caller.registerNameResponse(true);
		}

		public void sendMessage(string room, string message)
		{
			Clients.OthersInGroup(room).recieveMessageResponse(room, Name, message, false);
		}
		public void joinRoom(string room)
		{
			Groups.Add(GetConnectionId, room);


			Clients.OthersInGroup(room).recieveMessageResponse(room, Name, Name + " has joined the room" ,true);

			//	}
			Clients.Caller.joinRoomResponse(room, true);

			//}
		}
		public void leaveRoom(string room)
		{
			Groups.Remove(GetConnectionId, room);
			Clients.Group(room).recieveMessageResponse(room, Name, Name + " has left the room", true);

		}
		public void getAvailableRooms()
		{
			Clients.Caller.getAvailableRoomsResponse(Rooms.Keys.ToArray());
		}
		public void getCurrentRooms()
		{

			var rooms = (from v in Rooms
						 let user = (v.Value != null) ? v.Value.Users.Where(p => p.ConnectionId == Context.ConnectionId).FirstOrDefault() : null
						 where
						 user != null
						 select v.Key).Distinct().ToArray();

			Clients.Caller.getCurrentRoomsResponse(rooms);
		}
	}
}
