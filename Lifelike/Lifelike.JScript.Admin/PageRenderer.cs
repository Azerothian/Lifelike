using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using jQueryApi;

namespace Lifelike.JScript.Admin
{
	public class PageRenderer : Control
	{
		private static PageRenderer _context;

		public static PageRenderer Context
		{
			get
			{
				if (_context == null)
					_context = new PageRenderer();
				return _context;
			}
		}
		public PageRenderer() : base("body")
		{
			this.ControlContainer = jQuery.Select("body").GetElement(0);
		}

		public override void PreRender()
		{
			
			
		}
	}
}
