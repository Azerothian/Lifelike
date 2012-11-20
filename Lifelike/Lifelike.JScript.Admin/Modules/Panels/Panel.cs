using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using jQueryApi;
using jQueryApi.UI.Interactions;
using Lifelike.JScript.Admin.Managers;

namespace Lifelike.JScript.Admin.Modules.Panels
{
	public class Panel : Control
	{
		public DroppableOptions droppableOptions { get; set; }
		public Panel(string name)
			: base(name)
		{


			droppableOptions = new DroppableOptions()
			{
				Scope = "draggable",
				OnActivate = new jQueryApi.UI.jQueryUIEventHandler<DropActivateEvent>(OnActive),
				OnDeactivate = new jQueryApi.UI.jQueryUIEventHandler<DropDeactivateEvent>(OnDeactivate),
				OnDrop = new jQueryApi.UI.jQueryUIEventHandler<DropEvent>(OnDrop),
				OnOut = new jQueryApi.UI.jQueryUIEventHandler<jQueryObject>(OnOut),
				OnOver = new jQueryApi.UI.jQueryUIEventHandler<DropOverEvent>(OnOver)
			};
			CssClass = "droppable";
		}

		public override void PreRender()
		{
			//jQuery.FromElement(ControlContainer).CSS("position", "absolute");
			//jQuery.FromElement(ControlContainer).CSS("border", "solid");
		}
		public override void PostRender()
		{
			Util.Console().log(".modules.panels.panel.postrender ", droppableOptions, ControlContainer);
			jQuery.FromElement(ControlContainer).Droppable(droppableOptions);

			base.PostRender();
		}

		private void OnActive(jQueryEvent e, DropActivateEvent dae)
		{
			//Window.Alert("act");
		}
		private void OnDeactivate(jQueryEvent e, DropDeactivateEvent dae)
		{
			//Window.Alert("deact");
		}
		private void OnDrop(jQueryEvent e, DropEvent dae)
		{

			var id = dae.Draggable.GetAttribute("id");
			Util.Console().log(".modules.panels.panel.ondrop ", id);

			var control = (DockableControl)PageManager.Context.GetControlByClientId(id);
			DropControl(control);

		}
		private void OnOut(jQueryEvent e, jQueryObject dae)
		{
			//Window.Alert("out");
		}
		private void OnOver(jQueryEvent e, DropOverEvent dae)
		{
			//Window.Alert("over");
		}

		public void DropControl(DockableControl control)
		{
			int spacer = 5;
			var left = int.Parse(this.Left) + spacer;

			var top = int.Parse(this.Top) + spacer;

			control.Left = left + "px";
			control.Top = top + "px"; ;
			control.Width = this.Width - (spacer * 2);
			control.Height = this.Height - (spacer * 2);
		}
	}
}
