using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Saltarelle.SignalR;

namespace Lifelike.JScript.Admin
{

	public delegate void ChatMessage(string message);
	[IgnoreNamespace]
	[Imported(IsRealType = true)]
	public class ChatClient : Client
	{
		[IntrinsicProperty]
		public ChatMessage addMessage { get; set; }


	}
	[IgnoreNamespace]
	[Imported(IsRealType = true)]
	public class ChatServer : Server
	{
		public void sendMessage(string chat)
		{

		}
	}
}
