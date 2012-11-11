using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.EntityLogic;
using Telerik.Web.UI;

namespace Lifelike.WebAdmin.lifelike.Modules
{
	public partial class ItemEditor : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				RadTreeNode rtnItem = new RadTreeNode("Items", "") { ExpandMode = TreeNodeExpandMode.WebService };
				rtnItem.Attributes["nodetype"] = "item";
				RadTreeNode rtnDomain = new RadTreeNode("Domains") { ExpandMode = TreeNodeExpandMode.WebService };
				RadTreeNode rtnLayout = new RadTreeNode("Layouts") { ExpandMode = TreeNodeExpandMode.WebService };
				RadTreeNode rtnModule = new RadTreeNode("Modules") { ExpandMode = TreeNodeExpandMode.WebService };
				RadTreeNode rtnView = new RadTreeNode("Views") { ExpandMode = TreeNodeExpandMode.WebService };
				var session = Lifelike.Kernel.Database.Context.OpenSession();
				var item = ItemLogic.LoadAllBy(session, (p => p.Parent == null));

				//rtnItem = CreateTree(rtnItem, item.ToArray());

				rtvEntities.Nodes.Add(rtnItem);


				//var domains = DomainLogic.LoadAllBy(session);
				//foreach (var d in domains)
				//{
				//	rtnDomain.Nodes.Add(new RadTreeNode(d.BaseUri));
				//}
				rtvEntities.Nodes.Add(rtnDomain);

				//var layouts = LayoutLogic.LoadAllBy(session);
				//foreach (var d in layouts)
				//{
				//	rtnLayout.Nodes.Add(new RadTreeNode(d.Name));
				//}
				rtvEntities.Nodes.Add(rtnDomain);
				//var modules = ModuleLogic.LoadAllBy(session);
				//foreach (var d in modules)
				//{
				//	rtnModule.Nodes.Add(new RadTreeNode(d.Name));
				//}
				rtvEntities.Nodes.Add(rtnModule);
				//var views = ViewLogic.LoadAllBy(session);
				//foreach (var d in views)
				//{
				//	rtnView.Nodes.Add(new RadTreeNode(d.Name));
				//}
				rtvEntities.Nodes.Add(rtnView);
			}

		}
		//private static List<RadTreeNodeData> CreateNodes(
		[WebMethod]
		public static RadTreeNodeData[] GetNodes(RadTreeNodeData node)
		{
			var session = Lifelike.Kernel.Database.Context.OpenSession();
			string nodetype = (string)node.Attributes["nodetype"];
			List<RadTreeNodeData> result = new List<RadTreeNodeData>();
			switch (nodetype)
			{
				case "item":

					Item item = null;
					Guid guid = Guid.Empty;
					if (Guid.TryParse(node.Value, out guid))
					{
						item = ItemLogic.LoadBy(session, (p => p.Id == guid));
					}
					else
					{

						item = ItemLogic.LoadBy(session, (p => p.FullPath == "/"));
					}
					

					if (item.Children.Count > 0)
					{
						foreach (Item child in item.Children)
						{
							RadTreeNodeData childNode = new RadTreeNodeData();
							childNode.Text = child.Name;
							childNode.Value = child.Id.ToString();
							childNode.Attributes["nodetype"] = "item";
							//childNode.Attributes["path"] = child.FullPath;
							if (child.Children != null && child.Children.Count > 0)
							{
								childNode.ExpandMode = TreeNodeExpandMode.WebService;
							}
							result.Add(childNode);
						}
					}
					break;
			}


			return result.ToArray();
		}

		public RadTreeNode CreateTree(RadTreeNode node, Item[] item)
		{

			foreach (var i in item)
			{
				var n = new RadTreeNode(i.Name);
				if (i.Children != null && i.Children.Count() > 0)
				{
					CreateTree(n, i.Children.ToArray());
				}
				node.Nodes.Add(n);
			}
			return node;
		}

	}
}