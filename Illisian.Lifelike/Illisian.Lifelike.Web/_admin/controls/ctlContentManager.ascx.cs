using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Illisian.Lifelike.PresentationLogic.Interfaces;
using Illisian.Lifelike.PresentationLogic.Managers;

namespace Illisian.Lifelike._admin.controls
{
    public partial class ctlContentManager : System.Web.UI.UserControl, IContentManager
    {
        string itemid = "";

        protected ContentManager _manager;

        protected void Page_Init(object sender, EventArgs e)
        {
            _manager = new ContentManager(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest && !IsPostBack)
            {
                _manager.Initialise();
                SetupSitePanel();
            }
           
        }

        

        protected void NodeLoad(object sender, NodeLoadEventArgs e)
        {
            
            _manager.LoadNodeContents(sender, e);
        }

        protected void OnClick_tpContent(object sender, DirectEventArgs e)
        {
            itemid = e.ExtraParams["guid"];
        }

        protected void OnClick_btnCreate(object sender, DirectEventArgs e)
        {
            _manager.CreateNewItem();
         //   var wtf = (tpContents.GetSelectionModel());
           // var test = (TreeSelectionModel)wtf;
        }
        private void SetupSitePanel()
        {
            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "Root";
            tpContents.Root.Add(root);
            root.Children.Add(CreateNode("illisian.com.au"));
            root.Children.Add(CreateNode("propertyworks.com"));
            root.Children.Add(CreateNode("minecraftworld.net"));
            root.Children.Add(CreateNode("augaming.org"));
            root.Children.Add(CreateNode("nadir-game.com"));

        }
        private Node CreateNode(string name)
        {
            Guid id = Guid.NewGuid();
            Ext.Net.Node node = new Ext.Net.Node();
            node.NodeID = id.ToString();
            node.Text = name;
            node.CustomAttributes.Add(new ConfigItem("guid", id.ToString(), ParameterMode.Value));
            node.Leaf = false;
            return node;
        }

        //public Guid SelectedNode
        //{
        //    get {  }
        //}

        public string txtCreateField { get { return txtNewName.Text; } }
    }
}