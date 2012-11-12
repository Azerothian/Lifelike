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

	public class Label : HtmlControl
	{
		private Control _for;
		private string _text;

		public Control ForControl
		{
			get { return _for; }
			set { _for = value; SetAttribute("for", _for.ControlId); }
		}
		public string Text
		{
			get
			{
				if (IsRendered)
					return jQuery.Select("#" + ControlId).GetHtml();
				return _text;
			}
			set
			{
				if (IsRendered)
					jQuery.Select("#" + ControlId).Html(value);
				_text = value;
			}
		}
		

		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<label id='" + ControlId + "'>"+_text+"</label>");
		}
		protected override void Control_SetProperties()
		{

			base.Control_SetProperties();
			if (_for != null)
			{
				SetAttribute("for", _for.ControlId);
			}
			jQuery.Select("#" + ControlId).Html(_text);
		}

		
	}
}


