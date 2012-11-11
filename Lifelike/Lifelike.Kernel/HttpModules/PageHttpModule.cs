using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Lifelike.Kernel.EntityLogic;

namespace Lifelike.Kernel.HttpModules
{

    public class PageHttpModule : IHttpModule
    {
		HttpApplication _context;
        public PageHttpModule()
            : base()
        {
           

        }

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
			_context = context;
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
			context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
			//context.PostAcquireRequestState += context_PostAcquireRequestState;
			context.PostMapRequestHandler += context_PostMapRequestHandler;
			//context.PostAcquireRequestState += context_PostAcquireRequestState;
			context.PostUpdateRequestCache += context_PostUpdateRequestCache;
			context.PostResolveRequestCache += context_PostResolveRequestCache;
			
			context.AcquireRequestState += context_AcquireRequestState;
			context.UpdateRequestCache += context_UpdateRequestCache;
			context.PostRequestHandlerExecute += context_PostRequestHandlerExecute;
        }

		void context_PostRequestHandlerExecute(object sender, EventArgs e)
		{
		//	ItemLogic.ProcessModules();
		}

		void context_PostMapRequestHandler(object sender, EventArgs e)
		{
		}

		void context_AcquireRequestState(object sender, EventArgs e)
		{
			
		}

		void context_PostResolveRequestCache(object sender, EventArgs e)
		{
			//_context.Response.End();
		}

		void context_PostUpdateRequestCache(object sender, EventArgs e)
		{
			
			//_context.Response.End();
		}

		void context_UpdateRequestCache(object sender, EventArgs e)
		{


			
		}

		void context_PreRequestHandlerExecute(object sender, EventArgs e)
		{
			if (!(HttpContext.Current.Handler is Page))
			{
				return;
				//throw new Exception("Page base type not found");
			}
			Page page = HttpContext.Current.Handler as Page;
			page.PreInit += page_PreInit;

			//_context.Response.Flush();
			//_context.Response.End();
		}

		void page_PreInit(object sender, EventArgs e)
		{

			ItemLogic.ProcessModules();
		}

		

        void context_BeginRequest(object sender, EventArgs e)
        {
			ItemLogic.RewritePath(_context);
			
			//try
			//{
			
			//}
			//catch (Exception ex)
			//{
			//	//TODO: Change CreatePage to return diff exceptions, catc
			//}

            //var session = Database.CurrentSession;
            //var path = Context.CurrentDomain.BasePath + Language.SetAndRemoveLangaugeFromUrl(Context.Http.Request.Path.Replace("default.aspx", ""));
            //Context.Item = Database.GetItemByPath(path);

            /// Context.Http.Request.Path

            //foreach (RendererConfig cfg in Context.GetSystemConfig.Renderers)
            //{
            //    if (Context.Http.Request.Path == cfg.Path)
            //    {
            //        var asm = AppDomain.CurrentDomain.GetAssemblies().Where(p => p.FullName == cfg.AssemblyName).FirstOrDefault();
            //        if (asm != null)
            //        {
            //            var type = asm.GetType(cfg.Class);
            //            ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
            //            object responder = ci.Invoke(null);
            //            MethodInfo mi = type.GetMethod("Render");
            //            object[] parameters = new[] { Context.Http.HttpContext };
            //            mi.Invoke(responder, parameters);
            //        }
            //        else
            //        {
            //            // TODO: log asm not found
            //        }
            //    }

            //}


            //if (Context.Item != null)
            //{
            //    if (Context.Item.Layout != null)
            //    {
            //        var layout = Context.Item.Layout;
            //        Page page = (Page)System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath("~/" + layout.FilePath, typeof(Page));
            //        page.Init += new EventHandler(page_Init);
            //        page.ProcessRequest(Context.Http.HttpContext);
            //        Context.Http.HttpContext.Response.End();
            //        return;
            //    }
            //}
        }

        void page_Init(object sender, EventArgs e)
        {
            Page page = (Page)sender;
            //page.Form.Action = page.Request.Url.AbsolutePath;
            //foreach (var sl in (from v in Context.Item.ItemSublayoutMaps orderby v.Id select v))
            //{
            //    Control c = page.LoadControl("~/" + sl.Sublayout.FilePath);
            //    Placeholder p = Utilities.WebUtils.Controls.FindChildControlByTypeAndId<Placeholder>(page.Controls, sl.PlaceholderID);
            //    if (p != null)
            //    {
            //        p.AddControlAt(c, sl.Level);

            //    }
            //}
            //throw new NotImplementedException();
        }



        void context_EndRequest(object sender, EventArgs e)
        {

        }





    }
}
