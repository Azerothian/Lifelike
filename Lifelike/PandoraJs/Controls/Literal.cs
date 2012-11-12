// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using PandoraJs.Utils.Extension;
using PandoraJs.Enums;

namespace PandoraJs.Controls
{

	public class Literal : Control
	{
		private string _html;

		public string Html
		{
			get
			{
				if (IsRendered)
					return jQuery.Select("#" + ControlId).GetHtml();
				return _html;
			}
			set
			{
				if (IsRendered)
					jQuery.Select("#" + ControlId).Html(value);
				_html = value;
			}
		}
		

		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<div id='" + ControlId + "'>"+_html+"</label>");
		}
		protected override void Control_SetProperties()
		{

			base.Control_SetProperties();
			jQuery.Select("#" + ControlId).Html(_html);
		}

		
	}
}


