using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;

namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class MessengerControl : Control
	{
		TextBox _txtMessage;
		Button _btnSend;
		public MessengerControl(string name)
			: base(name)
		{
			_txtMessage = new TextBox("txtMessage");
			_btnSend = new Button("btnSend");
			_btnSend.Text = "Send";
			AddChild(_txtMessage);
			AddChild(_btnSend);
		}




		public override void PreRender()
		{
		}

	}
}
