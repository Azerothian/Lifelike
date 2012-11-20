using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;
using System.Text;
using jQueryApi;

namespace Lifelike.JScript.Admin.Controls
{

	public class TreeNode : Control
	{
		public string Id { get { return getProperties("id"); } private set { setProperties("id", value); } }
		public Tree Tree { get; set; }
		public string Value { get { return getProperties("value"); } set { setProperties("value", value); } }
		public string Text { get { return lblDetail.Text; } set { lblDetail.Text = value; } }
		public bool Expanded
		{
			get
			{
				var c = getProperties("expanded");
				if (c != null)
				{
					return bool.Parse(c);
				}
				return false;
			}
			set { setProperties("expanded", value.ToString()); }
		}
		public int Index { get { return int.Parse(getProperties("index")); } set { setProperties("index", value.ToString()); } }


		public Label lblDetail { get; set; }
		public Label lblImage { get; set; }
		public Tree InnerTree { get; set; }


		public TreeNode()
			: base("node")
		{
			ControlContainer = Document.CreateElement("li");
			Name = Guid.CreateGuid();
			lblImage = new Label("lblImage", "span");
			lblImage.CssClass = "ui-icon ui-icon-carat-1-e";

			AddChild(lblImage);

			lblDetail = new Label("lblDetail");
			lblDetail.CssClass = "label";

			AddChild(lblDetail);


			InnerTree = new Controls.Tree("InnerTree");
			AddChild(InnerTree);
		}

		public void Delete()
		{

			foreach (TreeNode n in InnerTree.Children)
			{
				RemoveChild(n);
			}

			RemoveChild(this);
		}


		public void Expand()
		{
			Expanded = true;

			foreach (TreeNode e in InnerTree.Children)
			{
				e.Visible = true;
			}

		}
		public void Close()
		{
			Expanded = false;
			foreach (TreeNode e in InnerTree.Children)
			{
				e.Close();
				e.Visible = false;
			}
		}

		//public void Render(System.Html.Element parent)
		//{
		//	if ( Parent == null ||  Parent.Expanded)
		//	{
		//		parent.AppendChild(ControlContainer);
		//		if (Children != null)
		//		{
		//			foreach (var e in Children)
		//			{
		//				e.Render(ContainerElement);

		//			}
		//		}
		//	}
		//}



		public override void PreRender()
		{

		}
		public override void PostRender()
		{
			jQuery.FromElement(lblImage.ControlContainer).Click(p =>
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
			if (!Expanded) { Visible = false; }
			base.PostRender();

		}
	}

}
