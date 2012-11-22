using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using Lifelike.JScript.Admin.Managers;

namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class MessengerControl : Control
	{
		
		public string Room { get; set; }
		public RoomControl RoomControl { get; set; }
		TextBox _txtMessage;
		Button _btnSend;
		public MessengerControl(string name)
			: base(name)
		{
			_txtMessage = new TextBox("txtMessage");
			_txtMessage.OnEnter += new jQueryApi.jQueryEventHandler(btnSend_OnClick);
			_btnSend = new Button("btnSend");
			_btnSend.OnClick += new jQueryApi.jQueryEventHandler(btnSend_OnClick);
			_btnSend.Text = "Send";
			AddChild(_txtMessage);
			AddChild(_btnSend);
			OnResize += MessengerControl_OnResize;
		}

		void MessengerControl_OnResize()
		{
			_txtMessage.Width = (int.Parse(Width) - 85) + "px";
		}


		public void btnSend_OnClick(jQueryApi.jQueryEvent e)
		{
			if (!string.IsNullOrEmpty(_txtMessage.Value))
			{
				HubManager.Context.ChatHub.sendMessage(Room, _txtMessage.Value);
				RoomControl.AddNewMessage(_txtMessage.Value, false);
				_txtMessage.Value = "";
			}
		}

		public override void PreRender()
		{
		}

	}
}
