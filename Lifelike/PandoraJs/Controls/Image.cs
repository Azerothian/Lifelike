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

	public class Image : HtmlControl
	{

		private string _alt;

		public string Alt
		{
			get
			{
				if (IsRendered)
					return jQuery.Select("#" + ControlId).GetAttribute("alt");
				return _alt;
			}
			set
			{
				if (IsRendered)
					jQuery.Select("#" + ControlId).Attribute("alt", value);
				_alt = value;
			}
		}
		private string _source;

		public string Source
		{
			get
			{
				if (IsRendered)
					return jQuery.Select("#" + ControlId).GetAttribute("src");
				return _source;
			}
			set
			{
				if (IsRendered)
					jQuery.Select("#" + ControlId).Attribute("src", value);
				_source = value;
			}
		}

		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<img id='" + ControlId + "'/>");
		}
		protected override void Control_SetProperties()
		{

			base.Control_SetProperties();
			if (_alt != null)
			{
				SetAttribute("alt", _alt);
			}
			if (_source != null)
			{
				SetAttribute("src", _source);
			}
		}

		
	}
}


