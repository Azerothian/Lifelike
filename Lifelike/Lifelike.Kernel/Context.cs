using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Lifelike.Data.Entities;
using Lifelike.Kernel.EntityLogic;
using Lifelike.Kernel.Util;
using Lifelike.Kernel.WebComponents;
using NHibernate;
using NHibernate.Linq;
namespace Lifelike.Kernel
{
	public class Context
	{
		//public static Item Item
		//{
		//	get
		//	{
		//		return Util.Data.LoadField<Item>("current-item");
		//	}

		//	set
		//	{
		//		Util.Data.SaveField("current-item", value);
		//	}
		//}
		//public static View CurrentView
		//{
		//	get
		//	{
		//		return Util.Data.LoadField<View>("current-view");
		//	}

		//	set
		//	{
		//		Util.Data.SaveField("current-view", value);
		//	}
		//}
		private static ItemLogic _itemLogic;
		public static ItemLogic ItemLogic
		{
			get
			{
				if (_itemLogic == null)
					_itemLogic = new ItemLogic();
				return _itemLogic;
			}
		}

		private static DomainLogic _domainLogic;
		public static DomainLogic DomainLogic
		{
			get
			{
				if (_domainLogic == null)
					_domainLogic = new DomainLogic();
				return _domainLogic;
			}
		}
		private static ViewLogic _viewLogic;
		public static ViewLogic ViewLogic
		{
			get
			{
				if (_viewLogic == null)
					_viewLogic = new ViewLogic();
				return _viewLogic;
			}
		}

		public static void Initialise()
		{
			Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Lifelike.Kernel.HttpModules.PageHttpModule));
			//  Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Lifelike.Kernel.HttpModules.DbSessionHttpModule));



			Database.Context.Configure(new[] { typeof(Item).Assembly });

		}
		
	}
}
