using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Kernel;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.EntityLogic;
using SignalR.Hubs;

namespace Lifelike.Logic.Admin.Hubs
{
	public class ItemHub : Hub
	{

		public Item GetItem(Guid id)
		{
			var session = Database.Context.CurrentSession;
			return ItemLogic.LoadBy(session, (p => p.Id == id));
		}
		public Item GetItem()
		{
			var session = Database.Context.CurrentSession;
			return ItemLogic.LoadBy(session, (p => p.Parent == null));
		}
	}
}
