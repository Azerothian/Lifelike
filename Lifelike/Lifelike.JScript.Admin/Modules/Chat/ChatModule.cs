using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Lifelike.JScript.Admin.Managers;
using System.Html;

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

				HubManager.Context.GetConnection().chat.client.recieveMessage = new Response<string, string, string>(recieveMessageResponse);
				HubManager.Context.GetConnection().chat.client.getCurrentRoomsResponse = new Response<List<string>>(getCurrentRoomsResponse);
				HubManager.Context.GetConnection().chat.client.getAvailableRoomsResponse = new Response<List<string>>(getAvailableRoomsResponse);
				HubManager.Context.GetConnection().chat.client.registerNameResponse = new Response<bool>(registerNameResponse);
				HubManager.Context.GetConnection().chat.client.joinRoomResponse = new Response<bool>(joinRoomResponse);
				Util.Console().log("registerName");
				registerName(PageManager.Context.Username);

		}

		public void joinRoomResponse(bool success)
		{
			Util.Console().log("joinRoomResponse" + success);
		}
		public void registerNameResponse(bool success)
		{
			Util.Console().log("joinRoom");
			joinRoom(PageManager.Context.Username);
		}
		public void recieveMessageResponse(string room, string user, string message)
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
		public void getCurrentRoomsResponse(List<string> rooms)
		{

		}
		public void getAvailableRoomsResponse(List<string> rooms)
		{

		}
		public void registerName(string name)
		{
			HubManager.Context.GetConnection().chat.server.registerName(PageManager.Context.Username);
		}

		public void sendMessage(string room, string message)
		{
			HubManager.Context.GetConnection().chat.server.sendMessage(room, message);
		}

		public void getCurrentRooms()
		{
			HubManager.Context.GetConnection().chat.server.getCurrentRooms();
		}
		public void getAvailableRooms()
		{
			HubManager.Context.GetConnection().chat.server.getAvailableRooms();
		}

		public void joinRoom(string room)
		{
			HubManager.Context.GetConnection().chat.server.joinRoom(room);
		}
		public void leaveRoom(string room)
		{
			HubManager.Context.GetConnection().chat.server.leaveRoom(room);
		}




		public override void PreRender()
		{
			
		}
	}
}
