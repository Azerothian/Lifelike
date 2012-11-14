using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;
using System.Text;
using jQueryApi;

namespace Lifelike.JScript.Admin.jQueryUI
{
	public class Tree
	{

		public Tree(string selector)
		{
			_treeCollection = new List<Node>();
			_element = jQuery.Select(selector).GetElement(0);
			
		}

		private Element _element;

		private Element _containerElement;
		private List<Node> _treeCollection;

		public void AddNode(Node parent, Node newnode)
		{
			newnode.Tree = this;
			if (parent != null)
			{
				newnode.Parent = parent;
				if (parent.Children == null)
				{
					parent.Children = new List<Node>();
				}
				parent.Children.Add(newnode);
			}
			else
			{
				_treeCollection.Add(newnode);
			}
		}

		public Node FindNode(List<Node> collection, Func<Node, bool> func)
		{
			foreach (var v in collection)
			{
				if (func(v))
				{
					return v;
				}
				else if (v.Children != null)
				{
					return FindNode(v.Children, func);

				}
			}
			return null;
		}


		public Node FindNode(Func<Node, bool> func)
		{
			return FindNode(_treeCollection, func);
		}

		public void RemoveNode(Node node)
		{
			node.Parent.Children.Remove(node);
		}
		//public void UpdateNode(Node node)
		//{

		//}
		public void RefreshTree(Node node)
		{
			//TODO: if visible then refresh
		}




		internal void Render()
		{

			createContainer();
			foreach (var v in _treeCollection)
			{
				v.Render(_containerElement);
			}
		}


		private void deleteContainer()
		{
			jQuery.FromElement(_containerElement).Remove();
		}
		private void createContainer()
		{
			_containerElement = Document.CreateElement("ul");
			_containerElement.SetAttribute("class", "tree");
			_element.AppendChild(_containerElement);
		}
	}
}
