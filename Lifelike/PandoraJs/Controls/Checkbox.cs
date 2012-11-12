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

	public class Checkbox : HtmlControl
	{
		private bool _checked;

		public bool Checked
		{
			get{

				if (IsRendered)
					return GetElement.Checked;
				return _checked;
			}
			set
			{
				if (IsRendered)
					GetElement.Checked = value;
				_checked = value;
			}
		}


		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<input id='" + ControlId + "' name='" + ControlId + "' type='checkbox' />");
		}
		protected override void Control_SetProperties()
		{
			base.Control_SetProperties();
		}
		private CheckBoxElement GetElement
		{
			get
			{
				if (IsRendered)
				{
					return (CheckBoxElement)jQuery.Select("#" + ControlId).GetElement(0);
				}
				else
				{
					return null;

				}

			}

		}
		
	}
}


