using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using jQueryApi;
using Lifelike.JScript.Admin.Controls;
using Lifelike.JScript.Admin.Interfaces;
using Lifelike.JScript.Admin.Managers;

namespace Lifelike.JScript.Admin
{
	

	public class LoginForm : Control, ILogin
	{
		public LoginForm(string name) : base(name) {
			_loginManager = new LoginManager(this);
			//_loginManager.LoginResponseEvent = new Response<bool>(LoginResponse);
		}
		internal void LoginResponse(bool success)
		{
			
			if (success)
			{
				PageRenderer.Context.RemoveChild(this);
			}
		}
		private LoginManager _loginManager;
		private Dialog dlgLoginForm { get; set; }
		private TextBox txtUsername { get; set; }
		private TextBox txtPassword { get; set; }
		private Button btnLogin { get; set; }

		public string Username { get { return txtUsername.Value; } set { txtUsername.Value = value; } }
		public string Password { get { return txtPassword.Value; } set { txtPassword.Value = value; } }
		public bool Remember { get { return false; } }
		public EventHandler OnClose;
	//	public CheckBoxElement chkRemember;

		public override void RemoveRender()
		{
			if (OnClose != null)
			{
				OnClose(this, null);
			}
			
			base.RemoveRender();
		}

		public override void PreRender()
		{
			dlgLoginForm = new Dialog("dlgLoginForm");
			dlgLoginForm.Options.AutoOpen = true;
			dlgLoginForm.Options.CloseOnEscape = false;
			dlgLoginForm.Options.Draggable = false;
			dlgLoginForm.Options.Modal = true;
			dlgLoginForm.Options.Title = "Login";
			dlgLoginForm.IsCloseable = false;


			txtPassword = new TextBox("txtPassword");
			txtUsername = new TextBox("txtUsername");
			txtUsername.Placeholder = "Username";
			txtPassword.Placeholder = "Password";

			btnLogin = new Button("btnLogin");
			btnLogin.Text = "LOGIN";

			btnLogin.OnClick += btnLogin_OnClick;

			dlgLoginForm.AddChild(txtUsername);
			dlgLoginForm.AddChild(txtPassword);
			dlgLoginForm.AddChild(btnLogin);
			AddChild(dlgLoginForm);
		}

		public void btnLogin_OnClick(jQueryEvent e)
		{
			if (string.IsNullOrEmpty(txtUsername.Value))
			{
				
				Window.Alert("Please enter in a username");
				return;
			}
			_loginManager.LoginUser();
		}
	}
}
