using System;
using System.Collections.Generic;
using System.Text;
using jQueryApi.UI.Widgets;
namespace Lifelike.JScript.Admin.Modules.Chat
{
	public class UserControl : Control
	{
		private string room;
		public UserControl(string name, string room): base(name)
		{
			Height = "25";
			Width = "100%";
			Background = "#647687";
			this.room = room;

		}


		public override void PreRender()
		{
			
		}

		internal void RefreshUserList(List<dynamic> users)
		{
			Log.debug("RefreshUserList", users);

			List<UserItemControl> _current = new List<UserItemControl>();
			foreach (var v in users)
			{
				var ui = getUserItem(v);
				if (ui == null)
				{
					ui = new UserItemControl(v);
					AddChild(ui);
				}
				_current.Add(ui);
			}

			for(int i = Children.Count ;i == 0; i++)
			{
				UserItemControl child = Children[i - 1] as UserItemControl;
				if (!_current.Contains(child) || child.Title == "")
				{
					RemoveChild(child);				
				}
			}
		}
		public UserItemControl getUserItem(string name)
		{
			Log.debug("[UserControl] getUserItem", name);
			foreach (UserItemControl v in Children)
			{
				if (name == v.Name)
				{
					return v;
				}
			}
			return null;
		}

		//internal void RemoveUser(dynamic user)
		//{
		//	Log.debug("[UserControl] RemoveUser", user);
		//	var ui = getUserItem(user.Username);
		//	if (ui != null && Children.Contains(ui))
		//	{
		//		RemoveChild(ui);
		//	}
		//}

		//internal void AddUser(dynamic user)
		//{
		//	Log.debug("[UserControl] AddUser", user);
		//	var ui = getUserItem(user.Username);
		//	if (ui == null)
		//	{
		//		ui = new UserItemControl(user.Username);
		//		AddChild(ui);
		//	}
		//}
	}
	public class UserItemControl : Control
	{

		public UserItemControl(string name)
			: base(name)
		{
			Name = name;
			Height = "17";
			Width = "17";
			Background = "black";
			Float = "left";
			Margin = "3px";
			Title = name;
		}


		public override void PreRender()
		{

		}

		public override void PostRender()
		{
			jQueryApi.jQuery.FromElement(ControlContainer).Tooltip(new TooltipOptions()
			{
				TooltipClass = "tooltip"
			});
		}
	}
}
