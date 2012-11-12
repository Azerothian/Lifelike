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

	public class UnorderedList : Control
	{
		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<ul id='" + ControlId + "'></ul>");
		}
		public override void AddChild(Control control)
		{
			if (!(control is ListItem))
			{
				Logging.Error("You cannot add anything but a ListItem to a UnorderedList control", new object[] { this, control });
				return;
			}
			base.AddChild(control);
		}
	}
	public class OrderedList : Control
	{
		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<ol id='" + ControlId + "'></ol>");
		}
		public override void AddChild(Control control)
		{
			if (!(control is ListItem))
			{
				Logging.Error("You cannot add anything but a ListItem to a OrderedList control", new object[] { this, control });
				return;
			}
			base.AddChild(control);
		}
	}

	public class ListItem : Control
	{
		private string _text = "";
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
			jQuery.Select("#" + Parent.ControlId).Append("<li id='" + ControlId + "'>" + _text + "</li>");
		}
	}
}
