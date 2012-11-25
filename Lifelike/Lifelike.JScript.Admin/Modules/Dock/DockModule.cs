using System;
using System.Collections.Generic;
using System.Html;
using System.Text;

namespace Lifelike.JScript.Admin.Modules.Dock
{
	public class DockModule : Control
	{

		StartMenu menuItem;

		public DockModule(string name)
			: base(name)
		{
			menuItem = new StartMenu("startmenu");
			AddChild(menuItem);
			Height = "50px";
			Width = "100%";
			Position = "absolute";
			Float = "left";
			Colour c1 = new Colour()
			{
				R = 145,
				G = 178,
				B = 210,
				A = 255,
				Position = 0
			};
			Colour c2 = new Colour()
			{
				R = 145,
				G = 178,
				B = 210,
				A = 128,
				Position = 50
			};
			Colour c3 = new Colour()
			{
				R = 97,
				G = 140,
				B = 179,
				A = 128,
				Position = 100
			};

			var msg = Util.GradientGenerator(new[] { c1, c2, c3 });
			Background = msg;
		}

		public override void PreRender()
		{
		}
	}
}
