using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Illisian.Lifelike.Logic.Interfaces
{
    public interface IContentManager
    {
        TreePanel tpContent { get; }
        TreePanel tpControl { get; }
        TreePanel tpTemplate { get; }

    }
}
