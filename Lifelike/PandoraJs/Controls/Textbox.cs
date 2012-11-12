// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
//using PandoraJs.Utils.Extension.jQueryPlugins;
using PandoraJs.Utils.Extension;
using PandoraJs.Enums;
using PandoraJs.Utils;

namespace PandoraJs.Controls
{

	public class Textbox : HtmlControl
	{
		private bool _enabled;

		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				_enabled = value;

				if (_enabled)
				{
					RemoveAttribute("disabled");
				}
				else
				{
					SetAttribute("disabled", "disabled");
				}
			}
		}
		private bool _isPasswordField = false;

		public bool IsPasswordField
		{
			get { return _isPasswordField; }
			set
			{
				if (IsRendered)
				{
					Logging.Log(LoggingType.Warning,"You cannot change the type of a input field once it is initialised", null);
				}
				else
				{
					_isPasswordField = value;
				}
			}
		}

		private string _placeholder = "";
		public string Placeholder
		{
			get { return _placeholder; }
			set
			{
				_placeholder = value;
				
				SetAttribute("placeholder", value);
				if (!Support.IsTextBoxPlaceholderSupported && IsInitialised)
				{
					//jQueryExtension.Select<jQueryPluginObject>("#" + ControlId).Placeholder();

				}
					//Logging.Log(LoggingType.Warning, "Placeholder attribute is not supported by the input tag", null);
			}
		}

		protected override void Control_Render()
		{
			string type = HtmlControlType.Text;
			if (_isPasswordField)
				type = HtmlControlType.Password;
			jQuery.Select("#" + Parent.ControlId).Append("<input id='" + ControlId + "' name='" + ControlId + "' type='" + type + "' />");
		}
		protected override void Control_SetProperties()
		{

			base.Control_SetProperties();
			SetAttribute("placeholder", _placeholder);
			
			if (!Support.IsTextBoxPlaceholderSupported)
			{
				//jQueryExtension.Select<jQueryPluginObject>("#" + ControlId).Placeholder();
				if (_enabled)
				{
					RemoveAttribute("disabled");
				}
				else
				{
					SetAttribute("disabled", "disabled");
				}
			}
		}
		public override string[] JavascriptFiles
		{
			get
			{
				if (!Support.IsTextBoxPlaceholderSupported)
					return new string[] { "jquery.placeholder" };
				return null;
			}
		}




	}
}


