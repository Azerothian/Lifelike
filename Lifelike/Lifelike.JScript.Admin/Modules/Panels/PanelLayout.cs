using System;
using System.Collections.Generic;
using System.Html;
using System.Text;

namespace Lifelike.JScript.Admin.Modules.Panels
{
	public class PanelLayout : Control
	{

		public Panel pnlLeftSide { get; set; }
		public Panel pnlRightSide { get; set; }
		public Panel pnlMiddle { get; set; }
		public Panel pnlBottom { get; set; }
		//public Panel pnlTopBar { get; set; }
		public PanelLayout(string name)
			: base(name)
		{
			pnlLeftSide = new Panel("pnlLeftSide");
			pnlRightSide = new Panel("pnlRightSide");
			pnlMiddle = new Panel("pnlMiddle");
			//pnlTopBar = new Panel("pnlTopBar");
			pnlBottom = new Panel("pnlBottom");
			Window.AddEventListener("resize", new ElementEventListener(Resize));

            Position = "absolute";
			PointerEvents = "none";

            Height = (Window.InnerHeight - 50 ) + "px";
            Width = Window.InnerWidth + "px";

			//AddChild(pnlTopBar);
			AddChild(pnlLeftSide);
			AddChild(pnlRightSide);
			AddChild(pnlMiddle);
			AddChild(pnlBottom);

		}

		public void Resize(ElementEvent e)
		{
			var w = jQueryApi.jQuery.Window.GetWidth();
			var h = jQueryApi.jQuery.Window.GetHeight();

			var top = 50;
			
			var bottomHeight = 225;

			var panelHeight = (h - top) - bottomHeight;
			var panelWidth = (int)(w * 0.20);

			var middleWidth = w - (panelWidth * 2);
			var rightsideLeft = middleWidth + panelWidth;
			

			Log.log("Sizes", w, h, top, bottomHeight, panelWidth, panelHeight, middleWidth, rightsideLeft);

			//pnlTopBar.Height = "75px";
			//pnlTopBar.Width = "100%";
			//pnlTopBar.Top = "0";
			//pnlTopBar.Left = "0";

			//pnlTopBar.Dockable = false;
			pnlLeftSide.Position = "absolute";
			pnlLeftSide.Height = panelHeight + "px";
			pnlLeftSide.Width = panelWidth + "px";
			pnlLeftSide.Top = top+"px";
			pnlLeftSide.Left = "0px";

			pnlMiddle.Width = middleWidth + "px";
			pnlMiddle.Top = top + "px";
			pnlMiddle.Left = panelWidth + "px";
			pnlMiddle.Height = panelHeight + "px";
			pnlMiddle.Position = "absolute";

			pnlRightSide.Height = panelHeight + "px";
			pnlRightSide.Width = panelWidth + "px";
			pnlRightSide.Top = top + "px";
			pnlRightSide.Left = rightsideLeft + "px";
			pnlRightSide.Position = "absolute";

			pnlBottom.Height = bottomHeight + "px";
	 		pnlBottom.Top = (panelHeight + top)+ "px";
			pnlBottom.Left = "0px";
			pnlBottom.Width = w + "px";
			pnlBottom.Position = "absolute";

			foreach (var c in Children)
			{
				c.Resize();
			}

			Resize();
		}



		public override void PreRender()
		{

		}
		public override void PostRender()
		{
			Resize(null);
			base.PostRender();
		}
	}
}
