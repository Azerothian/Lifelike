// TextArea.cs
//

using System;
using System.Collections.Generic;
using jQueryApi;

namespace PandoraJs.Controls
{
	public class TextArea : HtmlControl
	{
		private int _rows = 5;
		private int _columns = 80;
		private string _html;
		public int Rows
		{
			get
			{
				return _rows;
			}
			set
			{
				_rows = value;
				if (IsRendered)
					SetAttribute("rows", _rows.ToString());
			}
		}
		public int Columns
		{
			get
			{
				return _columns;
			}
			set
			{
				_columns = value;
				if (IsRendered)
					SetAttribute("cols", _columns.ToString());
			}
		}
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
			
			jQuery.Select("#" + Parent.ControlId).Append("<textarea id='" + ControlId + "'>" + "</textarea>");

		}

		protected override void Control_SetProperties()
		{
			SetAttribute("rows", _rows.ToString());
			SetAttribute("cols", _columns.ToString());
			jQuery.Select("#" + ControlId).Html(_html);
			base.Control_SetProperties();
		}

	}
}
