using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using Lifelike.JScript.Admin.Modules.Chat;
using Lifelike.JScript.Admin.Modules.Log;

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
		public bool IsLoggedIn { get; set; }
		public ConsoleModule ConsoleModule { get; set; }
		public ChatModule chatModule { get; set; }
		private LoginForm _loginForm;

		public PageManager()
		{
			_loginForm = new LoginForm("frmLogin");
			ConsoleModule = new ConsoleModule("console");
			chatModule = new ChatModule("chat");
		}

		public bool hasRendered { get; set; }
		
		public void Initialise()
		{

			//HubManager.Context.GetConnection().auth.client.loginResponse = new Response<string, bool>(LoginResponse);
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

		internal void LoginResponse(string username ,bool success)
		{

        //    Util.Console().log(".auth.client.loginResponse", username, success);
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
			PageRenderer.Context.AddChild(ConsoleModule);
			PageRenderer.Context.AddChild(chatModule);
			chatModule.Render();
			ConsoleModule.Render();
			chatModule.registerName(PageManager.Context.Username);
		}

	}
}
