using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.Logic.Interfaces;
using Ext.Net;

namespace Illisian.Lifelike.Logic.Managers
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
            if (!X.IsAjaxRequest)
            {

                this.SetupSitePanel();
            }
        }

        private void SetupSitePanel()
        {
            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "Root";
            inf.tpContent.Root.Add(root);
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
            node.CustomAttributes.Add(new ConfigItem("guid",id.ToString(), ParameterMode.Value));
            node.Leaf = false;
            return node;
        }

        public void LoadNodeContents(object sender, NodeLoadEventArgs e)
        {
            e.Nodes.Add(CreateNode("1"));
        }
    }
}
