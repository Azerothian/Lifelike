using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Illisian.Lifelike.Logic.Interfaces;
using Illisian.Lifelike.Logic.Managers;
using Ext.Net;

namespace Illisian.Lifelike._admin.controls
{
    public partial class ctlContentManager : System.Web.UI.UserControl, IContentManager
    {
        protected ContentManager _manager;

        protected void Page_Init(object sender, EventArgs e)
        {
            _manager = new ContentManager(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _manager.Initialise();
        }

        public Ext.Net.TreePanel tpContent
        {
            get { return this.tpContents;  }
        }

        public Ext.Net.TreePanel tpControl
        {
            get { return this.tpControls; }
        }

        public Ext.Net.TreePanel tpTemplate
        {
            get { return this.tpTemplates;  }
        }

        protected void NodeLoad(object sender, NodeLoadEventArgs e)
        {
            _manager.LoadNodeContents(sender, e);
        }
    }
}