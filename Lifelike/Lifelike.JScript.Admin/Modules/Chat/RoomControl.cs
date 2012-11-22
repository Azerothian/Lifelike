using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using jQueryApi.UI.Effects;
using jQueryApi.UI.Interactions;
using jQueryApi.UI.Widgets;
using jQueryApi;
using System.Collections;
using Lifelike.JScript.Admin.Managers;
using Lifelike.JScript.Admin.Modules.Panels;
namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class RoomControl : Control
	{
		public string User { get; set; }
		public string Colour { get; set; }
		private Label _title;
		private BaseControl _messageContainer;
		private DockableControl _dockable;
		private MessengerControl _messenger;
		private UserControl _userControl;

		public string Title { get { return _title.Text; } set { _title.Text = value; } }
		public RoomControl(string room)
			: base(room)
		{
			_dockable = new DockableControl("MessageContainer");
			_dockable.OnResize += _dockable_OnResize;
			_userControl = new UserControl("UserControl", room);
			//_dockable.Options = new jQueryApi.UI.Widgets.DialogOptions()
			//{
			//	AutoOpen = true,
			//	Title = ,
			//	Width = 350,
			//	Height = 375
				
			//};
			_dockable.Title = "Room - " + room;

			_messageContainer = new BaseControl("MessageContainer");
			_messageContainer.CssClass = "messageContainer";
			_messenger = new MessengerControl("Messenger");
			_messenger.RoomControl = this;
			_messenger.Room = room;
			_messenger.CssClass = "messenger";
			_dockable.AddChild(_userControl);
			_dockable.AddChild(_messageContainer);
			_dockable.AddChild(_messenger);
			AddChild(_dockable);
		}

		void _dockable_OnResize()
		{
			_messageContainer.Height = (int.Parse(_dockable.Height) - 119) + "px";
			_messenger.Width = _dockable.Width;
		}

		public void ResizeWindow(jQueryEvent e, ResizeEvent re)
		{

		}

		public override void PreRender()
		{
		}

		internal void AddNewMessage(string user, string message, bool isAlert)
		{
			Log.log(".js.modules.chat.roomcontrol AddNewMessage", user, message);
			var newcount =  _messageContainer.Children.Count + 1;
			MessageControl msg = null;
			if (_messageContainer.Children.Count > 1)
			{
				
				var m = _messageContainer.Children[_messageContainer.Children.Count - 2] as MessageControl;
				if (m.Username == user && !m.CssClass.EndsWith("alert") )
				{
					msg = m;
					msg.Message = msg.Message + " <br/>";
				}
			}
			if(msg == null)
			{
				msg = new MessageControl("message_" +newcount);
			}


			msg.Username = user;
			msg.Message = msg.Message + message;
			msg.Parent = _messageContainer;


		
			if (isAlert)
			{
				msg.CssClass = " chatmessageContainer alert";
			}
			else if (user != PageManager.Context.Username)
			{
				msg.CssClass = " chatmessageContainer outsider";
			}
			_messageContainer.Children.Add(msg);
			msg.Render();
			BaseControl spacer = new BaseControl("spacer_" + newcount);
			spacer.CssClass = "clear";
			spacer.Parent = _messageContainer;
			_messageContainer.Children.Add(spacer);
			spacer.Render();
			var result = ((_messageContainer.Children.Count /2) * 41);
			_messageContainer.ScrollDown();
			//Control.ScrollDown(result, _messageContainer.ControlContainer);


		}



		internal void AddNewMessage(string message, bool isAlert)
		{
			AddNewMessage(User, message, isAlert);
		}

		internal void RefreshUserList(List<dynamic> users)
		{
			_userControl.RefreshUserList(users);
		}

		//internal void AddUser(dynamic username)
		//{
		//	_userControl.AddUser(username);
		//}

		//internal void RemoveUser(dynamic username)
		//{
		//	_userControl.RemoveUser(username);
		//}
	}
}
