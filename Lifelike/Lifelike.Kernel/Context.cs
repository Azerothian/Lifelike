using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Lifelike.Entities;
using Lifelike.Kernel.EntityLogic;
using Lifelike.Kernel.Util;
using Lifelike.WebComponents;

namespace Lifelike.Kernel
{
	public class Context
	{
		public static Item Item
		{
			get
			{
				return Util.Data.LoadField<Item>("current-item");
			}

			set
			{
				Util.Data.SaveField("current-item", value);
			}
		}
		public static View CurrentView
		{
			get
			{
				return Util.Data.LoadField<View>("current-view");
			}

			set
			{
				Util.Data.SaveField("current-view", value);
			}
		}
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

		public static void Initialise()
		{
			Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Lifelike.Kernel.HttpModules.PageHttpModule));
			//  Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Lifelike.Kernel.HttpModules.DbSessionHttpModule));



			Database.Context.Configure(new[] { typeof(Item).Assembly });

		}
		public static void CreatePage()
		{

			var path = HttpContext.Current.Request.Path;
			var url = HttpContext.Current.Request.Url.AbsoluteUri;
			var host = HttpContext.Current.Request.Url.Host;


			var session = Lifelike.Kernel.Database.Context.OpenSession();
			CreateStructure();
			var domain = DomainLogic.LoadBy(session, new Func<Domain, bool>(i => i.BaseUri.ToLower() == host.ToLower()));
			if (domain == null)
			{
				domain = DomainLogic.LoadBy(session, new Func<Domain, bool>(i => i.BaseUri.ToLower() == ""));
			}
			if (domain == null)
			{
				throw new Exception("Domain's is missing. Unable to continue."); // TODO: Langauge Resource File?
//CreateStructure();
			}
			else
			{
				
			}

			Item item = null;
			if (url == "/" || url == "Default.aspx")
			{
				item = domain.StartItem;
			}
			else
			{
				item = ItemLogic.LoadBy(session, new Func<Item, bool>(i => i.FullPath.ToLower() == String.Format("{0}{1}", domain.StartItem.FullPath, path.Substring(1)).ToLower()));
			}
			if (item == null)
			{
				throw new Exception("Domain's start item is missing. Unable to continue."); // TODO: Langauge Resource File?
			}
			Context.Item = item;

			var view = item.Views.FirstOrDefault();

			if (view == null)
			{
				throw new Exception(String.Format("The Item '{0}' has no views to process. Unable to continue.", item.FullPath)); // TODO: Langauge Resource File?
			}

			Context.CurrentView = view;

			if (view.Layout == null)
			{
				throw new Exception(String.Format("The View '{0}' has no layout to process. Unable to continue.", view.Name));
			}

			Page page = (Page)System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath("~/" + view.Layout.Path, typeof(Page));
			page.Init += new EventHandler(page_Init);
			page.ProcessRequest(Http.HttpContext);
			Http.HttpContext.Response.End();
			
		}
		private static void page_Init(object sender, EventArgs e)
		{
			Page page = (Page)sender;
			page.Form.Action = page.Request.Url.AbsolutePath;

			var view = Context.CurrentView;
			foreach (var m in (from v in view.Modules orderby v.Id select v))
			{
				Control c = page.LoadControl("~/" + m.Module.Path);

				var t = c.GetType();
				//TODO: Load Data in to control properties

				Placeholder p = WebControls.FindChildControlByTypeAndId<Placeholder>(page.Controls, m.Placeholder);
				int i = 0;
				if (p != null)
				{
					i++;
					p.AddControlAt(c, i);
				}
			}
		}

		public static void CreateStructure()
		{
			var session = Lifelike.Kernel.Database.Context.OpenSession();
			using (var tx = session.BeginTransaction())
			{

				var item = new Item()
				{
					Name = "Home",
					Active = true
				};
				var domain = new Domain()
				{
					StartItem = item,
					BaseUri = "",
					Active = true
				};

				var layout = new Layout()
				{
					Name = "Main",
					Path = "/files/layouts/Main.aspx",
					Active = true
				};
				var module = new Lifelike.Entities.Module()
				{
					Name = "TestModule",
					Path = "/files/modules/TestModule.ascx",
					Active = true
				};
				var view = new View()
				{
					Name = "Web",
					Layout = layout,
					Active = true
					
				};
				var moduleViewMap = new HashSet<ModuleViewMap> { new ModuleViewMap() { View = view, Placeholder="test", Module = module ,
					Active = true } };

				view.Modules = moduleViewMap;
				item.Views = new HashSet<View> { view };


				item.Save(session, tx);
				domain.Save(session, tx);
				layout.Save(session, tx);
				module.Save(session, tx);
				foreach (var mvm in moduleViewMap)
				{
					mvm.Save(session, tx);
				}
				view.Save(session, tx);
				tx.Commit();
			}
		}
	}
}
