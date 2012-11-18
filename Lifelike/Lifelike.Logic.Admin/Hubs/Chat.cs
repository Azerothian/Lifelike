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
		public Dictionary<string, string> Usernames { get; set; }
		public Dictionary<string, Room> Rooms { get; set; }

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
			Clients.Caller.registerNameResponse(true);
		}

		public void sendMessage(string target, string message)
		{
			Clients.OthersInGroup(target).recieveMessage(message);
		}
		public void joinRoom(string room)
		{
			Groups.Add(GetConnectionId, room);


			Clients.OthersInGroup(room).recieveMessage(room, Name, Name + " has joined the room");

			//	}
			Clients.Caller.joinRoomResponse(true);

			//}
		}
		public void leaveRoom(string room)
		{
			Groups.Remove(GetConnectionId, room);
			Clients.Group(room).recieveMessage(room, Name, Name + " has left the room");

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
