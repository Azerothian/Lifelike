using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;
using System.Text;
using jQueryApi;
using Lifelike.JScript.Admin.Modules.Chat;

namespace Lifelike.JScript.Admin.Controls
{
	public class Tree : Control
	{
		public Tree(string name) : base (name)
		{
			ControlContainer = Document.CreateElement("ul");
			ControlContainer.SetAttribute("class", "tree");
		}
		
		//public void RemoveNode(TreeNode node)
		//{
		//	node.Parent.Children.Remove(node);
		//}

		public void RefreshTree(TreeNode node)
		{
			//TODO: if visible then refresh
		}



		public override void PreRender()
		{

		}
	}
}
