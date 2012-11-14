using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using SignalR;
using SignalR.Client.Hubs;
using SignalR.Hosting.Self;

namespace Lifelike.Logic.Admin
{
	public class HubManager
	{

		public void Initialise()
		{

			

			string url = "http://cms.illisian.com.au:15000/";
			var server = new Server(url);

			// Map the default hub url (/signalr)
			server.MapHubs();

			// Start the server
			server.Start();

		//	Console.WriteLine("Server running on {0}", url);


			Thread _thread = new Thread(Worker);
			_thread.Start();
			// Keep going until somebody hits 'x'
			
		//	var hubConnection = new HubConnection("http://localhost/mysite");

		//	// Create a proxy to the chat service
		//	var chat = hubConnection.CreateHubProxy("chat");

		//	// Print the message when it comes in
		//	chat.On("addMessage", message => Console.WriteLine(message));

		//	// Start the connection
		//	hubConnection.Start().Wait();
		}
		public bool Running { get; set; }
		public void Worker()
		{
			do
			{

				Thread.Sleep(100);

			} while (Running);
		}

	}
}
