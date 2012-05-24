using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.PresentationLogic.Interfaces;
using Ext.Net;

namespace Illisian.Lifelike.PresentationLogic.Managers
{
    public class ContentManager
    {

        IContentManager inf;

        public ContentManager(IContentManager _inf)
        {
            inf = _inf;
        }

        public void Initialise()
        {

        }

       


        public void LoadNodeContents(object sender, NodeLoadEventArgs e)
        {
           // e.Nodes.Add(CreateNode("1"));
        }

        public void CreateNewItem()
        {
           // throw new NotImplementedException();
        }
    }
}
