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
		public Chat()
		{
			//Rooms = new Dictionary<string, Room>();
			//Usernames = new Dictionary<string, string>();
		}


		private static Dictionary<string, string> _usernames;
		private static Dictionary<string, Room> _rooms;
		public static Dictionary<string, string> Usernames
		{
			get
			{
				if (_usernames == null)
				{
					_usernames = new Dictionary<string, string>();
				}
				return _usernames;
			}
		}
		
		public static Dictionary<string, Room> Rooms
		{
			get
			{
				if (_rooms == null)
				{
					_rooms = new Dictionary<string, Room>();
				}
				return _rooms;
			}
		}
		public string GetConnectionId { get { return this.Context.ConnectionId; } }



		public string Name
		{
			get
			{
				if (Usernames.ContainsKey(GetConnectionId))
				{
					return Usernames[GetConnectionId];
				}
				return "";

			}
			set
			{
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

		public void sendMessage(string room, string message)
		{
			Clients.OthersInGroup(room).recieveMessageResponse(room, Name, message, false);
		}
		public void joinRoom(string room)
		{
			if (!Rooms.ContainsKey(room))
			{
				Rooms.Add(room, new Room() { Name = room, Users = new List<string>() });
			}
			var u = Rooms[room].GetUser(Name);
			if (u == null)
			{
				Rooms[room].Users.Add(Name);
				Groups.Add(GetConnectionId, room);

				Clients.OthersInGroup(room).recieveMessageResponse(room, Name, Name + " has joined the room", true);

				Clients.OthersInGroup(room).userJoinedRoomResponse(room, Name);
				Clients.Caller.joinRoomResponse(room, true);
				Clients.OthersInGroup(room).getListOfUsersFromRoomResponse(room, Rooms[room].Users);
				getListOfUsersFromRoom(room);
			}
		}
		public void leaveRoom(string room)
		{
			if (!Rooms.ContainsKey(room))
			{
				Rooms.Add(room, new Room() { Name = room, Users = new List<string>() });
			}
			var u = Rooms[room].GetUser(Name);
			if (u != null)
			{
				Groups.Remove(GetConnectionId, room);
				Rooms[room].Users.Remove(Name);

				Clients.OthersInGroup(room).recieveMessageResponse(room, u, Name + " has left the room", true);
				Clients.OthersInGroup(room).userLeftRoomResponse(room, Name);
				Clients.OthersInGroup(room).getListOfUsersFromRoomResponse(room, Rooms[room].Users);
			}
		}
		public void getAvailableRooms()
		{
			Clients.Caller.getAvailableRoomsResponse(Rooms.Keys.ToArray());
		}
		public void getCurrentRooms()
		{

			var rooms = (from v in Rooms
						 let user = (v.Value != null) ? v.Value.Users.Where(p => p == Name).FirstOrDefault() : null
						 where
						 user != null
						 select v.Key).Distinct().ToArray();

			Clients.Caller.getCurrentRoomsResponse(rooms);
		}
		public void getListOfUsersFromRoom(string room)
		{
			if (Rooms.ContainsKey(room))
			{
				Clients.Caller.getListOfUsersFromRoomResponse(room, Rooms[room].Users);
			}

		}

	}
}
