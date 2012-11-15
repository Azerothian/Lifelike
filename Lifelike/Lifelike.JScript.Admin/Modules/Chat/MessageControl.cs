using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Controls;

namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class MessageControl : Control
	{
		ImageElement _avatar;
		Label _username;
		Label _message;


		public MessageControl(string name)
			: base(name)
		{
			//_avatar = (ImageElement)Document.CreateElement("img");
			_username = new Label("username");
			_message = new Label("message");

			_message.CssClass = "message";
			_username.CssClass = "username";
			//_avatar.CssClass = "avatar";

			//ControlContainer.AppendChild(_avatar);
			AddChild(_username);
			AddChild(_message);
			CssClass = "chatmessageContainer";
		}

		public string Username { get { return _username.Text; } set { _username.Text = value; } }
		public string Message { get { return _message.Text; } set { _message.Text = value; } }
		//public string ImageSrc { get { return _avatar.Src; } set { _avatar.Src = value; } }

		public override void PreRender()
		{

		}
	}
}
