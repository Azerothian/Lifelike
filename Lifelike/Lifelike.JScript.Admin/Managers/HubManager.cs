using System;
using System.Collections.Generic;
using System.Html;
using System.Runtime.CompilerServices;
using System.Text;
using Lifelike.JScript.Admin.Managers.Hubs;
using Saltarelle.SignalR;

namespace Lifelike.JScript.Admin.Managers
{
	public delegate void ChatMessage(string msg);
	public class HubManager
	{
		public Chat ChatHub { get; set; }
		public event Response<bool> OnConnection;
		private static HubManager _context;
		public static HubManager Context
		{
			get
			{
				if (_context == null) { _context = new HubManager(); }
				return _context;
			}
		}
		public HubManager()
		{
			ChatHub = new Chat();
		}
		public void Initialise()
		{
			//GetConnection().chat.addMessage = new ChatMessage(msg => {  }); 
			GetConnection().hub.logging = true;
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
           

			
			if (OnConnection != null)
				OnConnection(true);

			//Window.Alert("CONNECTED");
			//var chat = 	GetConnection().chat;
			//chat.server.sendMessage("HI!");
		}

		[InlineCode("$.connection")]
		public dynamic GetConnection()
		{

			return null;
		}

	}
}
