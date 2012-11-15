using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Modules.Chat;

namespace Lifelike.JScript.Admin.Managers
{
	public class PageManager
	{

		private static PageManager _context;
		public static PageManager Context
		{
			get
			{
				if (_context == null) { _context = new PageManager(); }
				return _context;
			}
		}
		public string Username { get; set; }

		private LoginForm _loginForm;

		public bool IsLoggedIn { get; set; }

		public PageManager()
		{
			_loginForm = new LoginForm("frmLogin");
		}

		public bool hasRendered { get; set; }
		
		public void Initialise()
		{
			HubManager.Context.GetConnection().auth.loginResponse = new Response<bool>(LoginResponse);
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
				PageRenderer.Context.AddChild(_loginForm);
			}
		}

		internal void LoginResponse(bool success)
		{
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
			var chatModule = new ChatModule("chat");
			PageRenderer.Context.AddChild(chatModule);
			chatModule.Render();
		}

	}
}
