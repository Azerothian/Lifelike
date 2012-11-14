using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;
using System.Text;
using jQueryApi;

namespace Lifelike.JScript.Admin.jQueryUI
{

	public class Node
	{

		private List<Node> _nodes;
		
		public Node()
		{
			Element = Document.CreateElement("li");
			LabelElement = Document.CreateElement("div");
			LabelElement.SetAttribute("class", "label");
			Image = Document.CreateElement("span");
			Image.SetAttribute("class", "ui-icon ui-icon-carat-1-e");

			jQuery.FromElement(Image).Click(p =>
			{

				if (Expanded)
				{
					Close();
				}
				else
				{
					Expand();
				}

			});


			TextElement = Document.CreateElement("div");

			LabelElement.AppendChild(Image);
			LabelElement.AppendChild(TextElement);
			Element.AppendChild(LabelElement);

			Id = Guid.CreateGuid();

		}


		public string Id { get { return getProperties(Element, "id"); } private set { setProperties(Element, "id", value); } }
		public Tree Tree { get; set; }
		public string Value { get { return getProperties(Element, "value"); } set { setProperties(Element, "value", value); } }
		public string Text { get { return jQuery.FromElement(TextElement).GetText(); } set { jQuery.FromElement(TextElement).Text(value); } }
		public bool Expanded {
			get
			{
				var c = getProperties(Element, "expanded");
				if (c != null)
				{
					return bool.Parse(c);
				}
				return false;
			}
			set { setProperties(Element, "expanded", value.ToString()); } }
		public int Index { get { return int.Parse(getProperties(Element, "expanded")); } set { setProperties(Element, "expanded", value.ToString()); } }

		public Element Element { get; set; }
		public Element Image { get; set; }
		public Element TextElement { get; set; }
		public Element ContainerElement { get; set; }
		public Element LabelElement { get; set; }
		public Node Parent { get; set; }
		public List<Node> Children
		{
			get
			{
				return _nodes;
			}
			set
			{

				if (value == null && _nodes != null)
				{

					foreach (var v in _nodes)
					{
						v.Delete();
					}
					deleteContainer();
				}
				else if (value != null && _nodes == null)
				{
					createContainer();
				}
				_nodes = value;
			}
		}

		private void deleteContainer()
		{
			jQuery.FromElement(ContainerElement).Remove();
		}
		private void createContainer()
		{
			ContainerElement = Document.CreateElement("ul");
			ContainerElement.SetAttribute("class", "container");
			Element.AppendChild(ContainerElement);
		}


		private string getProperties(Element element, string name)
		{
			return (string)element.GetAttribute("data-" + name);
		}
		private void setProperties(Element element, string name, string value)
		{
			element.SetAttribute("data-" + name, value);
		}

		public void Delete()
		{
			if (Children != null)
			{
				foreach (Node n in Children)
				{
					n.Delete();
				}
				Children = null;
			}
			jQuery.FromElement(Element).Remove();
			Parent.Children.Remove(this);
		}


		public void Expand()
		{
			Expanded = true;
			if (Children != null)
			{
				foreach (var e in Children)
				{
					e.Render(ContainerElement);
				}
			}
		}
		public void Close()
		{
			Expanded = false;
			if (Children != null)
			{
				foreach (var e in Children)
				{
					e.Close();
					jQuery.FromElement(e.Element).Remove();
				}
			}
			
		}

		public void Render(System.Html.Element parent)
		{
			if ( Parent == null ||  Parent.Expanded)
			{
				parent.AppendChild(Element);
				if (Children != null)
				{
					foreach (var e in Children)
					{
						e.Render(ContainerElement);

					}
				}
			}
		}
	}

}
