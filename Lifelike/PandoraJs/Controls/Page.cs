using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using PandoraJs.Utils.Extension;
using PandoraJs.Utils;

namespace PandoraJs.Controls
{

	public class Page : Control
	{


		private string _title;

		public string jQueryTheme = "Aristo";


		private static Page _context;
		public static Page Context
		{
			get
			{
				if (_context == null)
				{
					_context = new Page();
				}
				return _context;
			}
		}

		public Page()
			: base()
		{

			Id = "body";
			jQuery.Select("body").Attribute("id", Id);
		}

	
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
				Document.Title = _title;
			}
		}
		protected override void Control_Render()
		{

		}
		protected override void Control_SetProperties()
		{
			//base.Refresh();
		}
		public void ChangeTheme(string newTheme)
		{
			string oldTheme = CssFiles[0];
			Logging.Debug("Old Theme", new object[] { oldTheme });
			FileLoader.Context.RemoveCSS(oldTheme);
			jQueryTheme = newTheme;
			string _newTheme = CssFiles[0];
			Logging.Debug("New Theme", new object[] { _newTheme });
			FileLoader.Context.ProcessCSS(_newTheme, null);
		}

		public override string[] CssFiles
		{
			get
			{
				return new string[] { "../ui/" + jQueryTheme + "/jquery.ui" };
			}
		}
		public override string[] JavascriptFiles
		{
			get
			{
				return new string[] {
					"jquery.ui" };
			}
		}



		
	}
}
