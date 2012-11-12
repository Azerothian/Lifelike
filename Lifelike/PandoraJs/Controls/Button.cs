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

	public class Button : HtmlControl
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
					//jQueryExtension.Select<jQueryUIObject>("#" + ControlId).RemoveClass("ui-state-disabled");
				}
				else
				{
					SetAttribute("disabled", "disabled");
					//jQueryExtension.Select<jQueryUIObject>("#" + ControlId).AddClass("ui-state-disabled");
				}
			}
		}

		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<input id='" + ControlId + "' type='button' value='" + Value + "'/>");
			Enabled = _enabled; // TODO:  OMG so cheap will fix later :D
			
		}



	}
}
