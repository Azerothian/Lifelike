using System;
using System.Collections.Generic;
using jQueryApi;
using PandoraJs.Utils.Extension;
using System.Html;
using PandoraJs.Enums;



namespace PandoraJs
{
	public abstract class HtmlControl : Control
	{
		#region Private Fields
		private string _textDirection = TextDirection.Left;
		private int _tabIndex = 0;
		private string _title;
		private string _value;
		private string _fontSize = "";
		#endregion

		#region Public Properties
		public string Title
		{
			get { return _title; }
			set { _title = value; SetAttribute("title", value); }
		}

		public int TabIndex
		{
			get { return _tabIndex; }
			set { _tabIndex = value; SetAttribute("tabIndex", value.ToString()); }
		}



		public string Value
		{
			get { if (IsRendered) { return GetAttribute("value"); } return _value; }
			set
			{
				_value = value;
				SetAttribute("value", value);
			}
		}

		public string FontSize
		{
			get { return _fontSize; }
			set { _fontSize = value; if (IsRendered) { SetFontSize(); } }
		}

		private void SetFontSize()
		{
			if (!string.IsNullOrEmpty(_fontSize))
			{
				SetStyle("font-size", _fontSize);
			}
		}
		#endregion

		#region Events

		#endregion

		#region Functions
		public HtmlControl()
			: base()
		{

		}


		protected override void Control_SetProperties()
		{
			if (IsRendered)
			{
				base.Control_SetProperties();
				SetAttribute("title", _title);
				SetAttribute("tabIndex", _tabIndex.ToString());
				SetAttribute("value", _value);
				SetAttribute("dir", _textDirection);
				SetFontSize();
			}
		}
		protected override abstract void Control_Render();

		#endregion
	}

}
