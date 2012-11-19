﻿using System;
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
                HubManager.Context.GetConnection().chat.client.registerNameResponse = new Response<bool>(registerNameResponse);
				HubManager.Context.GetConnection().chat.client.recieveMessageResponse = new Response<string, string, string>(recieveMessageResponse);
				HubManager.Context.GetConnection().chat.client.getCurrentRoomsResponse = new Response<List<string>>(getCurrentRoomsResponse);
				HubManager.Context.GetConnection().chat.client.getAvailableRoomsResponse = new Response<List<string>>(getAvailableRoomsResponse);
				HubManager.Context.GetConnection().chat.client.joinRoomResponse = new Response<bool>(joinRoomResponse);
				registerName(PageManager.Context.Username);

		}

		public void joinRoomResponse(bool success)
		{
            Util.Console().log(".chat.client.joinRoomResponse", success);
		}
		public void registerNameResponse(bool success)
		{
            Util.Console().log(".chat.client.registerNameResponse" , success);
			joinRoom(PageManager.Context.Username);
		}
		public void recieveMessageResponse(string room, string user, string message)
		{
            Util.Console().log(".chat.client.recieveMessageResponse", room, user, message);
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
            Util.Console().log(".chat.client.registerNameResponse", rooms);
		}
		public void getAvailableRoomsResponse(List<string> rooms)
		{
            Util.Console().log(".chat.client.getCurrentRoomsResponse", rooms);
		}
		public void registerName(string name)
		{
            Util.Console().log(".chat.server.registerName", name);
			HubManager.Context.GetConnection().chat.server.registerName(PageManager.Context.Username);
		}

		public void sendMessage(string room, string message)
		{
            Util.Console().log(".chat.server.sendMessage", room, message);
			HubManager.Context.GetConnection().chat.server.sendMessage(room, message);
		}

		public void getCurrentRooms()
		{
            Util.Console().log(".chat.server.getCurrentRooms");
			HubManager.Context.GetConnection().chat.server.getCurrentRooms();
		}
		public void getAvailableRooms()
		{
            Util.Console().log(".chat.server.getAvailableRooms");
			HubManager.Context.GetConnection().chat.server.getAvailableRooms();
		}

		public void joinRoom(string room)
		{
            Util.Console().log(".chat.server.joinRoom", room);
			HubManager.Context.GetConnection().chat.server.joinRoom(room);
		}
		public void leaveRoom(string room)
		{
            Util.Console().log(".chat.server.leaveRoom", room);
			HubManager.Context.GetConnection().chat.server.leaveRoom(room);
		}




		public override void PreRender()
		{
			
		}
	}
}
