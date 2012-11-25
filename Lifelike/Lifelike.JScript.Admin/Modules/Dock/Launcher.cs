using System;
using System.Collections.Generic;
using System.Text;
using Lifelike.JScript.Admin.Controls;

namespace Lifelike.JScript.Admin.Modules.Dock
{
	public class Launcher : Control
	{
		Label lblDisplay { get; set; }
		public Launcher(string name)
			: base(name)
		{
			lblDisplay = new Label("launcherLabel");
			Height = "40px";
			Width = "50px";
			Margin = "5px";
			Float = "left";
			lblDisplay.Border = "2px solid #000";
		}

		public Control Remote { get; set; }

		public void launcher_Click(jQueryApi.jQueryEvent e)
		{
			if (Remote != null)
			{
				Remote.Visible = !Remote.Visible;
			}
		}


		public string Text { get { return lblDisplay.Text; } set { lblDisplay.Text = value; } }

		public override void PreRender()
		{
			AddChild(lblDisplay);
		}
		public override void PostRender()
		{
			OnClick(launcher_Click);
			base.PostRender();
		}
	}
}
