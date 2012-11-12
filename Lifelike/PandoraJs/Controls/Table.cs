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

	public class Table : Control
	{
		private TableHeader _header;
		public TableHeader Header
		{
			get
			{
				if (_header == null)
				{
					_header = new TableHeader();
					_header.Id = "tableHeader";
					base.AddChild(_header);
				}
				return _header;
			}
		}
		private TableBody _body;
		public TableBody Body
		{
			get
			{
				if (_body == null)
				{
					_body = new TableBody();
					_body.Id = "tableBody";
					base.AddChild(_body);
				}
				return _body;
			}
		}

		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<table id='" + ControlId + "'></table>");
		}
		public override void AddChild(Control control)
		{
			if (control is TableBody)
			{
				if (_body != null)
				{
					Logging.Error("The TableBody has already been added (you can only have one), Have you accessed the 'Body' property?", new object[] { this, control });
					return;
				}
				_body = (TableBody)control;
				base.AddChild(control);
				return;
			}

			if (control is TableHeader)
			{
				if (_header != null)
				{
					Logging.Error("The TableHeader has already been added (you can only have one), Have you accessed the 'Header' property?", new object[] { this, control });
					return;
				}
				_header = (TableHeader)control;
				base.AddChild(control);
				return;
			}
			if (control is TableRow)
			{
				Body.AddChild(control);
				return;
			}
			if (control is TableHeaderColumn)
			{
				Header.AddChild(control);
				return;
			}
			Logging.Error("You cannot add non table types to the Table control, valid types are TableBody, TableHeader, TableRow, TableHeaderColumn.", new object[] { this, control });

		}

		internal void ClearBody()
		{
			if (_body != null)
			{
				_body.Unload();
				_body = null;
			}
		}
	}
	public class TableRow : Control
	{
		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<tr id='" + ControlId + "'></tr>");
		}
		public override void AddChild(Control control)
		{
			if (!(control is TableColumn))
			{
				Logging.Error("You cannot add anything but a TableColumn to a Table control", new object[] { this, control });
				return;
			}

			base.AddChild(control);
		}
	}

	public class TableHeader : Control
	{
		protected override void Control_Render()
		{
			if (HasChildren)
			{
				jQuery.Select("#" + Parent.ControlId).Append("<thead id='" + ControlId + "'><tr></tr></thead>");
			}
		}
		public override void AddChild(Control control)
		{
			if (!(control is TableHeaderColumn))
			{
				Logging.Error("You cannot add anything but a TableHeaderColumn to a TableHeader control", new object[] { this, control });
				return;
			}

			base.AddChild(control);
		}


	}

	public class TableBody : Control
	{
		protected override void Control_Render()
		{
			if (HasChildren)
			{
				jQuery.Select("#" + Parent.ControlId).Append("<tbody id='" + ControlId + "'></tbody>");
			}
		}
		public override void AddChild(Control control)
		{
			if (!(control is TableRow))
			{
				Logging.Error("You cannot add anything but a TableRow to a TableBody control", new object[] { this, control });
				return;
			}

			base.AddChild(control);
		}
	}

	public class TableHeaderColumn : TableColumn
	{
		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId + " tr").Append("<th id='" + ControlId + "'>" + Text + "</th>");
		}

	}
	public class TableColumn : Control
	{
		private string _text;
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
			jQuery.Select("#" + Parent.ControlId).Append("<td id='" + ControlId + "'>" + _text + "</td>");
		}

	}
}
