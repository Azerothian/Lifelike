using System;
using System.Collections.Generic;
using System.Html;
using System.Runtime.CompilerServices;
using System.Text;
using Saltarelle.SignalR;

namespace Lifelike.JScript.Admin
{
	public delegate void ChatMessage(string msg);
	public class HubManager
	{
		
		public void Initialise()
		{
GetConnection().chat.addMessage = new ChatMessage(msg => { Window.Alert(msg); });
			GetConnection().hub.start()
				.done(new EventHandler((object sender, EventArgs e) => { Connected(); }))
				.fail(new EventHandler((object sender, EventArgs e) => { Failed(); }));
		}

		private void Failed()
		{
			Window.Alert("FAILED to connect");
		}


		private void Connected()
		{

			Window.Alert("CONNECTED");

			GetConnection().chat.sendMessage("HI!");
		}

		[InlineCode("$.connection")]
		public dynamic GetConnection()
		{

			return null;
		}
		[InlineCode("debugger;")]
		public dynamic Debugger()
		{

			return null;
		}
		
	}
}
