using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Lifelike.JScript.Admin.Managers;

namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class ChatModule : Control
	{
		private List<RoomControl> Rooms { get; set; }


		public ChatModule(string name)
			: base(name) {
				//_element = Document.CreateElement("a");
				//ControlContainer.AppendChild(_element);
				Rooms = new List<RoomControl>();
				CssClass = "chatbar";

				HubManager.Context.GetConnection().chat_recieveMessage = new Response<string, string, string>(Process_recieveMessage);
				HubManager.Context.GetConnection().chat_getCurrentRoomsResponse = new Response<List<string>>(Process_getCurrentRoomsResponse);
				HubManager.Context.GetConnection().chat_getAvailableRoomsResponse = new Response<List<string>>(Process_getAvailableRoomsResponse);
				joinRoom(PageManager.Context.Username, PageManager.Context.Username);

		}

		public void Process_recieveMessage(string room, string user, string message)
		{
			RoomControl roomEnt = null;
			foreach (var v in Rooms)
			{
				if (v.Name == room)
				{
					roomEnt = v;
					break;
				}
			}
			if (roomEnt == null)
			{
				roomEnt = new RoomControl(room);
				Children.Add(roomEnt);
				roomEnt.Render();
			}
			roomEnt.AddNewMessage(user, message);


		}
		public void Process_getCurrentRoomsResponse(List<string> rooms)
		{

		}
		public void Process_getAvailableRoomsResponse(List<string> rooms)
		{

		}


		public void sendMessage(string target, string message)
		{
			HubManager.Context.GetConnection().chat.sendMessage(target, message);
		}

		public void getCurrentRooms()
		{
			HubManager.Context.GetConnection().chat.getCurrentRooms();
		}
		public void getAvailableRooms()
		{
			HubManager.Context.GetConnection().chat.getAvailableRooms();
		}

		public void joinRoom(string username, string target)
		{
			HubManager.Context.GetConnection().chat.joinRoom(username, target);
		}
		public void leaveRoom(string username, string target)
		{
			HubManager.Context.GetConnection().chat.leaveRoom(username, target);
		}




		public override void PreRender()
		{
			
		}
	}
}
