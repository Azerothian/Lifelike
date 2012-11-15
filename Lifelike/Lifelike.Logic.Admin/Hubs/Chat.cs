using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lifelike.Logic.Admin.Hubs.ChatLogic;
using SignalR.Hubs;

namespace Lifelike.Logic.Admin.Hubs
{
	public class Chat : Hub, IDisconnect, IConnected
	{

		public Dictionary<string, Room> Rooms { get; set; }

		public Task sendMessage(string target, string message)
		{
			return Clients.Group(target).recieveMessage(message);
		}
		public void joinRoom(string username, string room)
		{
			if (Rooms == null)
				Rooms = new Dictionary<string, Room>();
			if (!Rooms.Keys.Contains(room))
			{
				Rooms.Add(room, new Room() { Name = room, Users = new List<User>() });
			}
			if (Rooms[room].Users.Where(p => p.ConnectionId == Context.ConnectionId).Count() == 0)
			{
				//Groups.Add(Context.ConnectionId, name);
				Rooms[room].Users.Add(new User() { ConnectionId = Context.ConnectionId, Username = username });
				foreach (var u in Rooms[room].Users)
				{
					Clients.Client(u.ConnectionId).chat_recieveMessage(username + " has joined the room");

				}

				
			}
		}
		public void leaveRoom(string username , string room)
		{
			if (Rooms == null)
				Rooms = new Dictionary<string, Room>();
			if (Rooms.Keys.Contains(room) && Rooms[room].Name == room)
			{
				var user = Rooms[room].Users.Where(p => p.ConnectionId == Context.ConnectionId).FirstOrDefault();
				if (user != null)
				{
					Rooms[room].Users.Remove(user);
					foreach (var u in Rooms[room].Users)
					{
						Clients.Client(u.ConnectionId).recieveMessage(username + " has left the room");
					}
				}
			}

		}
		public void getAvailableRooms()
		{
			Caller.chat.getAvailableRoomsResponse(Rooms.Keys.ToArray());
		}
		public void getCurrentRooms()
		{

			var rooms = (from v in Rooms
						let user = (v.Value != null) ? v.Value.Users.Where(p => p.ConnectionId == Context.ConnectionId) .FirstOrDefault() : null
						where
						user != null
						select v.Key).Distinct().ToArray();

			Caller.chat.getCurrentRoomsResponse(rooms);
		}


		public System.Threading.Tasks.Task Connect()
		{
			return Clients.joined(Context.ConnectionId, DateTime.Now.ToString());
		}

		public System.Threading.Tasks.Task Reconnect(IEnumerable<string> groups)
		{
			return Clients.rejoined(Context.ConnectionId, DateTime.Now.ToString());
		}

		public System.Threading.Tasks.Task Disconnect()
		{
			return Clients.leave(Context.ConnectionId, DateTime.Now.ToString());
		}
	}
}
