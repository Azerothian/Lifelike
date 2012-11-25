using System;
using System.Collections.Generic;
using System.Text;

namespace Lifelike.JScript.Admin.Managers.Hubs
{
	public class Chat
	{
		public event Response<bool> OnRegisterNameResponse;
		public event Response<string, dynamic> OnUserJoinedRoomResponse;
		public event Response<string, dynamic> OnUserLeftRoomResponse;
		public event Response<string, string, string, bool> OnRecieveMessage;
		public event Response<string, bool> OnJoinRoomResponse;
		
		public event Response<List<string>> OnAvailableRoomsResponse;
		public event Response<List<string>> OnCurrentRoomsResponse;
		public event Response<string, List<dynamic>> OnListOfUsersFromRoomResponse;


		public Chat()
		{
			HubManager.Context.GetConnection().chat.client.registerNameResponse = new Response<bool>(registerNameResponse);
			HubManager.Context.GetConnection().chat.client.userLeftRoomResponse = new Response<string, dynamic>(userLeftRoomResponse);
			HubManager.Context.GetConnection().chat.client.userJoinedRoomResponse = new Response<string, dynamic>(userJoinedRoomResponse);
			HubManager.Context.GetConnection().chat.client.recieveMessageResponse = new Response<string, dynamic, string, bool>(recieveMessageResponse);
			HubManager.Context.GetConnection().chat.client.joinRoomResponse = new Response<string, bool>(joinRoomResponse);
			HubManager.Context.GetConnection().chat.client.getAvailableRoomsResponsee = new Response<List<string>>(getAvailableRoomsResponse);
			HubManager.Context.GetConnection().chat.client.getCurrentRoomsResponsee = new Response<List<string>>(getCurrentRoomsResponse);
			HubManager.Context.GetConnection().chat.client.getListOfUsersFromRoomResponse = new Response<string, List<dynamic>>(getListOfUsersFromRoomResponse);
			Log.log(".chat.init.complete");
		}

		private void userJoinedRoomResponse(string room, dynamic user)
		{
			Log.sockets(".chat.client.userJoinedRoomResponse", room, user);

			if (OnUserJoinedRoomResponse != null)
			{
				OnUserJoinedRoomResponse(room, user);
			}

		}
		private void userLeftRoomResponse(string room, dynamic user)
		{
			Log.sockets(".chat.client.userLeftRoomResponse", room, user);

			if (OnUserLeftRoomResponse != null)
			{
				OnUserLeftRoomResponse(room, user);
			}

		}
		private void recieveMessageResponse(string room, dynamic user, string message, bool isAlert)
		{
			Log.sockets(".chat.client.recieveMessageResponse", room, user, message, isAlert);
			if (OnRecieveMessage != null)
			{
				OnRecieveMessage(room, user, message, isAlert);
			}
		}

		public void joinRoomResponse(string room, bool success)
		{
			Log.sockets(".chat.client.joinRoomResponse", room, success);
			if (OnJoinRoomResponse != null)
			{
				OnJoinRoomResponse(room, success);
			}

		}
		public void registerNameResponse(bool success)
		{
			Log.sockets(".chat.client.registerNameResponse", success);
			if (OnRegisterNameResponse != null)
			{
				OnRegisterNameResponse(success);
			}
		}

		public void getCurrentRoomsResponse(List<string> rooms)
		{
			Log.sockets(".chat.client.getCurrentRoomsResponse", rooms);
			if (OnCurrentRoomsResponse != null)
			{
				OnCurrentRoomsResponse(rooms);
			}
		}
		public void getAvailableRoomsResponse(List<string> rooms)
		{
			Log.sockets(".chat.client.getCurrentRoomsResponse", rooms);
			if (OnAvailableRoomsResponse != null)
			{
				OnAvailableRoomsResponse(rooms);
			}
		}

		public void getListOfUsersFromRoomResponse(string room, List<dynamic> users)
		{
			Log.sockets(".chat.client.getListOfUsersFromRoomResponse", users);
			if (OnListOfUsersFromRoomResponse != null)
			{
				OnListOfUsersFromRoomResponse(room, users);
			}
		}




		public void registerName(string name)
		{
			Log.sockets(".chat.server.registerName", name);
			HubManager.Context.GetConnection().chat.server.registerName(PageManager.Context.Username);
		}

		public void sendMessage(string room, string message)
		{
			Log.sockets(".chat.server.sendMessage", room, message);
			HubManager.Context.GetConnection().chat.server.sendMessage(room, message);
		}
		public void getListOfUsersFromRoom(string room)
		{
			Log.sockets(".chat.server.getListOfUsersFromRoom", room);
			HubManager.Context.GetConnection().chat.server.getListOfUsersFromRoom(room);
		}
		public void getCurrentRooms()
		{
			Log.sockets(".chat.server.getCurrentRooms");
			HubManager.Context.GetConnection().chat.server.getCurrentRooms();
		}
		public void getAvailableRooms()
		{
			Log.sockets(".chat.server.getAvailableRooms");
			HubManager.Context.GetConnection().chat.server.getAvailableRooms();
		}

		public void joinRoom(string room)
		{
			Log.sockets(".chat.server.joinRoom", room);
			HubManager.Context.GetConnection().chat.server.joinRoom(room);
		}
		public void leaveRoom(string room)
		{
			Log.sockets(".chat.server.leaveRoom", room);
			HubManager.Context.GetConnection().chat.server.leaveRoom(room);
		}


	}
}
