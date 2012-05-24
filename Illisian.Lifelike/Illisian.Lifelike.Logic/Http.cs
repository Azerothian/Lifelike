using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Illisian.Lifelike.Logic
{

        public static class Http
        {
            public static HttpSessionState Session
            {
                get
                {
                    return HttpContext.Session;
                }
            }
            public static HttpRequest Request
            {
                get
                {

                    return HttpContext.Request;
                }
            }
            public static HttpResponse Response
            {
                get
                {
                    return HttpContext.Response;
                }
            }
            public static HttpContext HttpContext
            {
                get
                {
                    return HttpContext.Current;
                }
            }
            public static HttpApplication HttpApplication
            {
                get
                {
                    return HttpContext.Current.ApplicationInstance;
                }
            }

        }
    
}
