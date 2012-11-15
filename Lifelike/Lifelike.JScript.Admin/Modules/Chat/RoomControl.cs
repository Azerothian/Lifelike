using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Controls;

namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class RoomControl : Control
	{
		public string Colour { get; set; }

		private Label _title;
		private BaseControl _messageContainer;
		private MessengerControl _messenger;
		public string Title { get { return _title.Text; } set { _title.Text = value; } }
		public RoomControl(string name)
			: base(name)
		{
			_messageContainer = new BaseControl("MessageContainer");
			_messageContainer.CssClass = "messageContainer";
			_messenger = new MessengerControl("Messenger");
			_messenger.CssClass = "messenger";
			_title = new Label(name);
			_title.CssClass = "title";
			_title.Text = name;
			AddChild(_title);
			AddChild(_messageContainer);
			AddChild(_messenger);
		}
		
		

		public override void PreRender()
		{
		}

		internal void AddNewMessage(string user, string message)
		{
			MessageControl msg = new MessageControl("message" + _messageContainer.Children.Count + 1);
			msg.Username = user;
			msg.Message = message;
			_messageContainer.Children.Add(msg);
			msg.Render();
		}
	}
}
