using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.Entities.Xml;
using Lifelike.Kernel.Fields;
using Lifelike.Kernel.Util;
using Lifelike.Kernel.WebComponents;
using NHibernate;
using NHibernate.Linq;

namespace Lifelike.Kernel.EntityLogic
{
	public class ItemLogic : LogicAbstract<Item>
	{

		public static Item GetCurrentItem()
		{
			var path = HttpContext.Current.Request.Path;
			var url = HttpContext.Current.Request.RawUrl;
			var host = HttpContext.Current.Request.Url.Host;

			var session = Lifelike.Kernel.Database.Context.OpenSession();
			var domain = DomainLogic.GetCurrentDomain(host, session);

			if (domain == null)
			{
				throw new Exception("Domain's is missing. Unable to continue."); // TODO: Langauge Resource File?
			}
			var item = GetCurrentItem(url, domain, session);// ItemLogic.GetCurrentItem(host, path, domain, session);
			if (item == null)
			{
				//throw new Exception("Item not found");
			}
			//String.Format("{0}{1}", domain.StartItem.FullPath, path.Substring(1)).ToLower());
			return item;
		}

		public static Item GetCurrentItem(string path, Domain domain, ISession session)
		{
			Item item = null;
			if (path == "/" || path.ToLower() == "/default.aspx")
			{
				item = domain.StartItem;
			}
			else
			{
				string data = String.Format("{0}/{1}", domain.StartItem.FullPath, path.Substring(1)).ToLower();
				item = GetItemFromPath(data); // LoadBy(session, new Func<Item, bool>(i => i.FullPath != null && i.FullPath.ToLower() == );
			}
			return item;
		}



		public static void RewritePath(HttpApplication context)
		{

			var path = HttpContext.Current.Request.Path;
			var url = HttpContext.Current.Request.Url.AbsoluteUri;

			var host = HttpContext.Current.Request.Url.Host;

			//CreateStructure();

			var session = Lifelike.Kernel.Database.Context.OpenSession();
			//CreateStructure();

			//var domain = DomainLogic.GetCurrentDomain(host, session);

			var item = ItemLogic.GetCurrentItem();

			//if (domain == null)
			//{
			//	throw new Exception("Domain's is missing. Unable to continue."); // TODO: Langauge Resource File?
			//}

			if (item == null)
			{
				return; //TODO: Error 404
				//throw new Exception("Domain's start item is missing. Unable to continue."); // TODO: Langauge Resource File?
			}

			//Context.Item = item;

			var view = ViewLogic.GetCurrentView(item);

			if (view == null)
			{
				throw new Exception(String.Format("The Item '{0}' has no views to process. Unable to continue.", item.FullPath)); // TODO: Langauge Resource File?
			}

			//Context.CurrentView = view;

			if (view.Layout == null)
			{
				throw new Exception(String.Format("The View '{0}' has no layout to process. Unable to continue.", view.Name));
			}
			HttpContext.Current.RewritePath("~/" + view.Layout.Path);
			//Page page = (Page)System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath("~/" + view.Layout.Path, typeof(Page));
			//page.AppRelativeVirtualPath = context.Request.AppRelativeCurrentExecutionFilePath;
			//page.Init += new EventHandler(page_Init);
			//page.ProcessRequest(Http.HttpContext);
			//httpCon
			//context.




		}




		public static void ProcessModules()
		{
			if (!(HttpContext.Current.Handler is Page))
			{
				return;
				//throw new Exception("Page base type not found");
			}
			Page page = HttpContext.Current.Handler as Page;

			//page.Form.Action = HttpContext.Current.Request.Url.AbsolutePath;
			var item = ItemLogic.GetCurrentItem();
			if (item == null)
				return;
			var data = Util.Serialisation.Xml.Generics.DeserializeObjectFromString<TemplateData>(item.Value);
			//page.__templateData = data.Properties;

			Reflection.SetProperties(page, data.Properties);

			var view = ViewLogic.GetCurrentView(item);
			foreach (var m in (from v in view.Modules orderby v.Id select v))
			{
				var properties = (from v in data.PropertyGroups where v.Name == m.Module.Name select v).FirstOrDefault();

				var c = ModuleLogic.LoadModule(page, m.Module, properties.Properties);

				Placeholder p = WebControls.FindChildControlByTypeAndWithFilter<Placeholder>(page.Controls, (fc => fc.Key == m.Placeholder));
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

				DeleteAll(session, tx);
				tx.Commit();
			}
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

				var layout = new Lifelike.Kernel.Entities.Layout()
				{
					Name = "Main",
					Path = "/lifelike/layouts/Main.aspx",
					Active = true
				};
				var module = new Lifelike.Kernel.Entities.Module()
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

				item.Value = TemplateLogic.CreateTemplateFromView(view);

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

		private static void DeleteAll(ISession session, ITransaction tx)
		{
			foreach (var v in session.Query<ModuleViewMap>())
			{
				v.Delete(session, tx);
			}
			foreach (var v in session.Query<View>())
			{
				v.Delete(session, tx);
			}
			foreach (var v in session.Query<Lifelike.Kernel.Entities.Module>())
			{
				v.Delete(session, tx);
			}
			foreach (var v in session.Query<Lifelike.Kernel.Entities.Layout>())
			{
				v.Delete(session, tx);
			}
			foreach (var v in session.Query<Domain>())
			{
				v.Delete(session, tx);
			}
			foreach (var v in session.Query<Item>())
			{
				v.Delete(session, tx);
			}
		}

		public static Item GetItemFromPath(string path, bool create = false)
		{

			var session = Lifelike.Kernel.Database.Context.OpenSession();
			using (var tx = session.BeginTransaction())
			{
				return GetItemFromPath(session, tx, path, create);
			}
		}

		public static Item GetItemFromPath(ISession session, ITransaction tx, string path, bool create = false)
		{
			var item = ItemLogic.LoadBy(session, (p => p.FullPath == path));
			if (item == null && create)
			{
				var arr = new[] { "/" }.Concat(path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
				string pat = "";
				Item parent = null;
				foreach (var v in arr)
				{
					if (v != "/")
					{
						pat = pat + "/" + v;
					}
					else
					{
						pat = v;
					}
					var nw = ItemLogic.LoadBy(session, (p => p.FullPath == pat));
					if (nw == null)
					{
						nw = new Item()
						{
							Parent = parent,
							Name = v,
							Active = true,
							FullPath = pat
						};
						nw.Save(session, tx);
					}
					parent = nw;
				}
				item = ItemLogic.LoadBy(session, (p => p.FullPath == path));


				tx.Commit();
			}
			return item;
		}
	}
}
