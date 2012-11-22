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
		RoomControl roomEnt;

		public ChatModule(string name)
			: base(name) {
				//_element = Document.CreateElement("a");
				//ControlContainer.AppendChild(_element);
				Rooms = new List<RoomControl>();
				CssClass = "chatbar";
				HubManager.Context.ChatHub.OnRecieveMessage += ChatHub_OnRecieveMessage;
				HubManager.Context.ChatHub.OnJoinRoomResponse += ChatHub_OnJoinRoomResponse;
				HubManager.Context.ChatHub.OnRegisterNameResponse += ChatHub_OnRegisterNameResponse;
				HubManager.Context.ChatHub.OnUserJoinedRoomResponse += ChatHub_OnUserJoinedResponse;
				HubManager.Context.ChatHub.OnListOfUsersFromRoomResponse += ChatHub_OnListOfUsersFromRoomResponse;
				HubManager.Context.ChatHub.OnUserLeftRoomResponse += ChatHub_OnUserLeftRoomResponse;
				OnResize += ChatModule_OnResize;

				//HubManager.Context.GetConnection().chat.client.recieveMessageResponse = new Response<string, string, string, bool>(recieveMessageResponse);
				//HubManager.Context.GetConnection().chat.client.getCurrentRoomsResponse = new Response<List<string>>(getCurrentRoomsResponse);
				//HubManager.Context.GetConnection().chat.client.getAvailableRoomsResponse = new Response<List<string>>(getAvailableRoomsResponse);
				//HubManager.Context.GetConnection().chat.client.joinRoomResponse = new Response<string, bool>(joinRoomResponse);
				//HubManager.Context.GetConnection().chat.client.registerNameResponse = new Response<bool>(registerNameResponse);

		}

		void ChatModule_OnResize()
		{

		}

		private RoomControl getRoom(string name)
		{
			foreach (var v in Rooms)
			{
				if (name == v.Name)
				{
					return v;
				}
			}
			return null;
		}

		void ChatHub_OnListOfUsersFromRoomResponse(string room, List<dynamic> users)
		{
			getRoom(room).RefreshUserList(users);
		}

		void ChatHub_OnUserJoinedResponse(string room, dynamic username)
		{
			//getRoom(room).AddUser(username);
		}

		void ChatHub_OnUserLeftRoomResponse(string room, dynamic username)
		{
			//getRoom(room).RemoveUser(username);
		}
		void ChatHub_OnJoinRoomResponse(string room, bool success)
		{
			if (success)
			{
				createRoom(room);
	//			ChatHub_OnUserJoinedResponse(room, PageManager.Context.Username);
				HubManager.Context.ChatHub.getListOfUsersFromRoom(room);
			}
		}

		void ChatHub_OnRecieveMessage(string room, string user, string message, bool isAlert)
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
				Log.log("the room entity we are looking for is null ", room, Rooms, user, message, isAlert);
				return;
			}
			roomEnt.AddNewMessage(user, message, isAlert);
		}

		void ChatHub_OnRegisterNameResponse(bool success)
		{
			HubManager.Context.ChatHub.joinRoom("General");
		}

		public void createRoom(string room)
		{
			roomEnt = new RoomControl(room);
			roomEnt.Parent = this;
			roomEnt.User = PageManager.Context.Username;
			Children.Add(roomEnt);
			roomEnt.Render();
			Rooms.Add(roomEnt);
		}

		public override void PreRender()
		{
			
		}
	}
}
