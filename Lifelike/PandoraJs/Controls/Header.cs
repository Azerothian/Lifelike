// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using PandoraJs.Utils.Extension;
using PandoraJs.Enums;
using PandoraJs.Utils;

namespace PandoraJs.Controls
{

	public class Header : HtmlControl
	{
		private int _level = 1;

		private string _text = "";

		public int Level
		{
			get
			{
				return _level;
			}
			set
			{
				if (IsRendered)
				{
					Logging.Log(LoggingType.Error,"Unable to change header level once control is initialised. There is no browser restriction for this capability, needs to be built.", null);
				}
				else
				{
					_level = value;
				}
			}
		}
		public string Text
		{
			get
			{
				if (IsRendered)
					return jQuery.Select("#" + ControlId).GetText();
				return _text;
			}
			set
			{
				if (IsRendered)
					jQuery.Select("#" + ControlId).Text(value);
				_text = value;
			}
		}
		

		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<h"+_level.ToString()+" id='" + ControlId + "'>"+_text+"</h"+_level+">");
		}
		protected override void Control_SetProperties()
		{
			base.Control_SetProperties();
			jQuery.Select("#" + ControlId).Text(_text);
		}

		
	}
}


