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
namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class RoomControl : Control
	{
		public string User { get; set; }
		public string Colour { get; set; }
		private Label _title;
		private BaseControl _messageContainer;
		private Dialog _dialog;
		private MessengerControl _messenger;
		public string Title { get { return _title.Text; } set { _title.Text = value; } }
		public RoomControl(string room)
			: base(room)
		{
			_dialog = new Dialog("MessageContainer");
			_dialog.Options = new jQueryApi.UI.Widgets.DialogOptions()
			{
				AutoOpen = true,
				Title = "Room - " + room,
				Width = 350,
				Height = 375
				
			};
			

			_messageContainer = new BaseControl("MessageContainer");
			_messageContainer.CssClass = "messageContainer";
			_messenger = new MessengerControl("Messenger");
			_messenger.RoomControl = this;
			_messenger.Room = room;
			_messenger.CssClass = "messenger";
			_dialog.AddChild(_messageContainer);
			_dialog.AddChild(_messenger);
			AddChild(_dialog);
		}

		public void ResizeWindow(jQueryEvent e, ResizeEvent re)
		{

		}

		public override void PreRender()
		{
		}

		internal void AddNewMessage(string user, string message, bool isAlert)
		{
			Util.Console().log(".js.modules.chat.roomcontrol AddNewMessage", user, message);
			var newcount =  _messageContainer.Children.Count + 1;
			MessageControl msg = new MessageControl("message_" +newcount);
			msg.Username = user;
			msg.Message = message;
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
			var result = ((_messageContainer.Children.Count /2) * 41) + "px";
			Util.Console().log("result = ", result);
			JsDictionary _dictionary = new JsDictionary("scrollTop", result);


			jQueryApi.jQuery.FromElement(_messageContainer.ControlContainer).Animate(_dictionary, EffectDuration.Slow, EffectEasing.Linear);


			


		}

		internal void AddNewMessage(string message, bool isAlert)
		{
			AddNewMessage(User, message, isAlert);
		}
	}
}
