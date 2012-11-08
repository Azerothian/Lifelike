using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Kernel.Entities;
using NHibernate;

namespace Lifelike.Kernel.EntityLogic
{
	public class ItemLogic : LogicAbstract<Item>
	{

		public void RegisterItemSavingEvent()
		{
			//Item.
		
		}

		void Item_SavingEntityEvent(Item e)
		{
			
		}

		public Item GetCurrentItem(string host, string path, Domain domain, ISession session)
		{
			Item item = null;
			if (path == "/" || path.ToLower() == "/default.aspx")
			{
				item = domain.StartItem;
			}
			else
			{
				item = LoadBy(session, new Func<Item, bool>(i => i.FullPath != null && i.FullPath.ToLower() == String.Format("{0}{1}", domain.StartItem.FullPath, path.Substring(1)).ToLower()));
			}
			return item;
		}
	}
}
