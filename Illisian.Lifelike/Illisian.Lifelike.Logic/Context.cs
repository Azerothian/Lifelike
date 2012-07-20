using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.Data;

namespace Illisian.Lifelike.Logic
{
    public class Context
    {


        public static void Initialise()
        {
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Illisian.Lifelike.Logic.HttpModules.PageHttpModule));
          //  Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Illisian.Lifelike.Logic.HttpModules.DbSessionHttpModule));



           // Database.Context.Configure( new [] { typeof(BaseEntity).Assembly });

        }

    }
}
