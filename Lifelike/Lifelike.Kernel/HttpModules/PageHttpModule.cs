using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Lifelike.Kernel.HttpModules
{

    public class PageHttpModule : IHttpModule
    {

        public PageHttpModule()
            : base()
        {
           

        }

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {

            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
			Context.CreatePage();

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
