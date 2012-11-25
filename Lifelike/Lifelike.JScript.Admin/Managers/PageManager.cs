using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Modules.Chat;
using Lifelike.JScript.Admin.Modules.Console;
using Lifelike.JScript.Admin.Modules.Dock;
using Lifelike.JScript.Admin.Modules.Item;
using Lifelike.JScript.Admin.Modules.Panels;

namespace Lifelike.JScript.Admin.Managers
{
	public class PageManager
	{

		private static PageManager _context;
		public static PageManager Context
		{
			get
			{
				if (_context == null) { 
					_context = new PageManager();
				}
				return _context;
			}
		}
		public string Username { get; set; }
		public bool IsLoggedIn { get; set; }
		public ConsoleModule ConsoleModule { get; set; }
		public ChatModule chatModule { get; set; }
		public PanelLayout panelLayout { get; set; }
		public ItemTreeModule itemTreeModule { get; set; }
		public DockModule dockModule { get; set; }
		private LoginForm _loginForm;

		public PageManager()
		{
			
		}

		public bool hasRendered { get; set; }

		public void Initialise()
		{
			Log.log("Creating Controls..");
			Log.log("Loading LoginForm");
			_loginForm = new LoginForm("frmLogin");
			Log.log("Loading DockModule");
			dockModule = new DockModule("dock");
            Log.log("Loading Layout");
			panelLayout = new PanelLayout("pnlLayout");
			Log.log("Loading Console");
			ConsoleModule = new ConsoleModule("console");
			Log.log("Loading Chat");
			chatModule = new ChatModule("chat");
			Log.log("Loading Item Tree");
			itemTreeModule = new ItemTreeModule("items");

			Check();

		}
		public void Check()
		{
			LoginCheck();

			Window.SetTimeout(Check, 500);
		}


		public void LoginCheck()
		{
			if (!IsLoggedIn && !_loginForm.IsRendered)
			{
				Log.log("Initialising LoginForm");
				PageRenderer.Context.AddChild(_loginForm);
			}
		}

		internal void LoginResponse(string username, bool success)
		{

			Log.log("IsLoggedIn", username , success);
			//    Log.log(".auth.client.loginResponse", username, success);
			IsLoggedIn = success;
			if (success)
			{
				Username = _loginForm.Username;
				PageRenderer.Context.RemoveChild(_loginForm);
				if (!hasRendered)
				{
					InitateSystem(); // DUN DUN DAAAAHHH
				}
			}
		}
		public void InitateSystem()
		{

			Log.log("Initialising Main System");
			PageRenderer.Context.AddChild(dockModule);
			PageRenderer.Context.AddChild(panelLayout);
			PageRenderer.Context.AddChild(itemTreeModule);
			PageRenderer.Context.AddChild(ConsoleModule);
			PageRenderer.Context.AddChild(chatModule);
			
			PageRenderer.Context.Render();


			HubManager.Context.ChatHub.registerName(PageManager.Context.Username);
		}
		public Control GetControlByClientId(string id)
		{
			return PageRenderer.Context.FindControlByClientId(id);
		}

	}
}
