using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;
using Lifelike.JScript.Admin.Modules.Panels;

namespace Lifelike.JScript.Admin.Modules.Item
{
	public class ItemTreeModule : Control
	{
		DockableControl panel;
		Tree treeItems;
		public ItemTreeModule(string name)
			: base(name)
		{
			panel = new DockableControl("Items");
			panel.Title = "Items";

			treeItems = new Tree("treeItems");
			//Tree tree = new Tree(".itemEditor");

			var node = new TreeNode() { Text = "Text", Value = "Value", Expanded = true };
			node.AddChildren(new List<Control>()
			 {
				 new TreeNode() { Text = "Text", Value = "Value" , Parent = node },
				 new TreeNode() { Text = "Text", Value = "Value", Parent = node },
				 new TreeNode() { Text = "Text", Value = "Value", Parent = node },

			 });
			//var ss = new TreeNode() { Text = "Text", Value = "Value", Parent = node };
			//ss.AddChildren(new List<Control>()
			// {
			//	 new TreeNode() { Text = "Text", Value = "Value" , Parent = node },
			//	 new TreeNode() { Text = "Text", Value = "Value", Parent = node },
			//	 new TreeNode() { Text = "Text", Value = "Value", Parent = node },

			// });
			//node.Children.Add(ss);
			treeItems.AddChild(node);
			panel.AddChild(treeItems);
			AddChild(panel);
		}
		public override void PreRender()
		{
		}
	}
}
