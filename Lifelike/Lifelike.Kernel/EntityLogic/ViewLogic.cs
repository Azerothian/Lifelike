﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Kernel.Entities;
using NHibernate;

namespace Lifelike.Kernel.EntityLogic
{
	public class ViewLogic : LogicAbstract<View>
	{
		public View GetCurrentView(Item i)
		{
			return i.Views.FirstOrDefault();
		}
	}
}
