// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using PandoraJs.Utils.Extension;
using PandoraJs.Utils;

namespace PandoraJs.Controls
{

	public class LinkButton : HtmlControl
	{
		private bool _enabled = true;

		public virtual bool Enabled
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

		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<a href='javascript:;' id='" + ControlId + "' '>"+Value + "</a>");
			Enabled = _enabled; // TODO:  OMG so cheap will fix later :D
			
		}



	}
}
