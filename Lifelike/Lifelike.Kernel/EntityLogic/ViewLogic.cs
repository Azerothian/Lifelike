using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Kernel.Entities;
using NHibernate;

namespace Lifelike.Kernel.EntityLogic
{
	public class ViewLogic : LogicAbstract<View>
	{
		public static View GetCurrentView(Item i)
		{
			var view = i.Views.FirstOrDefault();
			if (view == null)
				throw new Exception("View not found in item");
			return view;
		}
	}
}
