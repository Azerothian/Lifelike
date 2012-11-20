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
		public PanelLayout(string name)
			: base(name)
		{
			pnlLeftSide = new Panel("pnlLeftSide");
			pnlRightSide = new Panel("pnlRightSide");
			pnlMiddle = new Panel("pnlMiddle");
			pnlBottom = new Panel("pnlBottom");
			Window.AddEventListener("resize", new ElementEventListener(Resize));


			AddChild(pnlLeftSide);
			AddChild(pnlRightSide);
			AddChild(pnlMiddle);
			AddChild(pnlBottom);

		}

		public void Resize(ElementEvent e)
		{
			var w = jQueryApi.jQuery.Window.GetWidth();
			var h = jQueryApi.jQuery.Window.GetHeight();


			var bottomHeight = 250;

			var bottomTop = h - bottomHeight;
			var sidePanelWidth = (int)(w * 0.20);

			var middleWidth = w - (sidePanelWidth * 2);
			var rightsideLeft = middleWidth + sidePanelWidth;


			pnlLeftSide.Height = bottomTop;
			pnlLeftSide.Width = sidePanelWidth;
			pnlLeftSide.Top = "0px";
			pnlLeftSide.Left = "0px";

			pnlMiddle.Width = middleWidth;
			pnlMiddle.Top = "0px";
			pnlMiddle.Left = sidePanelWidth + "px";
			pnlMiddle.Height = bottomTop;

			pnlRightSide.Height = bottomTop;
			pnlRightSide.Width = sidePanelWidth;
			pnlRightSide.Top = "0px";
			pnlRightSide.Left = rightsideLeft + "px";

			pnlBottom.Height = bottomHeight;
			pnlBottom.Top = bottomTop + "px";
			pnlBottom.Left = "0px";
			pnlBottom.Width = w;


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
