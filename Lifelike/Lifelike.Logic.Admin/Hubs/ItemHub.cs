﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Data.Entities;
using Lifelike.Kernel;
using Lifelike.Kernel.EntityLogic;
using Microsoft.AspNet.SignalR.Hubs;


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
