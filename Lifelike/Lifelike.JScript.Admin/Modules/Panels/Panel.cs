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

		public static Dictionary<string, Control> _currentDocked = new Dictionary<string, Control>();
		public DroppableOptions droppableOptions { get; set; }
		public Panel(string name)
			: base(name)
		{
			Dockable = true;
			OnResize += Panel_OnResize;
			droppableOptions = new DroppableOptions()
			{
				Scope = "draggable",
				OnActivate = new jQueryApi.UI.jQueryUIEventHandler<DropActivateEvent>(OnActive),
				OnDeactivate = new jQueryApi.UI.jQueryUIEventHandler<DropDeactivateEvent>(OnDeactivate),
				OnDrop = new jQueryApi.UI.jQueryUIEventHandler<DropEvent>(OnDrop),
				OnOut = new jQueryApi.UI.jQueryUIEventHandler<jQueryObject>(OnOut),
				OnOver = new jQueryApi.UI.jQueryUIEventHandler<DropOverEvent>(OnOver)
			};
			CssClass = " ";
//			Margin = "
			Position = "absolute";
		}

		void Panel_OnResize()
		{

			if (_currentDocked.ContainsKey(Name) && _currentDocked[Name] != null)
			{
				_currentDocked[Name].Resize();
			}
		}

		public override void PreRender()
		{
			//jQuery.FromElement(ControlContainer).CSS("position", "absolute");
			//jQuery.FromElement(ControlContainer).CSS("border", "solid");
		}
		public override void PostRender()
		{
			Log.log(".modules.panels.panel.postrender ", droppableOptions, ControlContainer);
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

			Log.log(".modules.panels.panel.test_ondrop ", Dockable);
			if (Dockable)
			{
				var id = dae.Draggable.GetAttribute("id");
				Log.log(".modules.panels.panel.ondrop ", id);

				var control = PageManager.Context.GetControlByClientId(id);
				Util.Debugger();
				if (!_currentDocked.ContainsKey(Name) || _currentDocked[Name] == null)
				{
					DropControl(control);
					
				}
			}

		}
		private void OnOut(jQueryEvent e, jQueryObject dae)
		{
			//Window.Alert("out");
		}
		private void OnOver(jQueryEvent e, DropOverEvent dae)
		{
			//Window.Alert("over");
		}

		public void DropControl(Control control)
		{
			int spacer = 5;
			var left = int.Parse(this.Left) + spacer;

			var top = int.Parse(this.Top) + spacer;

			control.Left = left + "px";
			control.Top = top + "px"; ;
			control.Width = (int.Parse(this.Width) - (spacer * 2)).ToString();
			control.Height = (int.Parse(this.Height) - (spacer * 2)).ToString();
			AddChild(control);

			foreach (var v in _currentDocked.Keys)
			{
				if (_currentDocked[v] == control)
				{
					_currentDocked.Remove(v);
					break;
				}
			}
			
			if (!_currentDocked.ContainsKey(Name))
			{
				_currentDocked.Add(Name, control);
			}
			else
			{
				_currentDocked[Name] = control;
			}
			
		}

		public bool Dockable { get; set; }
	}
}
