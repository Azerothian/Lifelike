using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Kernel.Fields
{
	public class Text : IField
	{

		private System.Web.UI.WebControls.TextBox _control;

		public Text()
		{
			_control = new System.Web.UI.WebControls.TextBox();
		}



		public object Value
		{
			get
			{
				return _control.Text;
			}
			set
			{

				if (value is String)
				{
					_control.Text = (String)value;
				}
				else
				{
					throw new ArgumentException("Must be a string object"); //TODO: Better Error message!
				}
					 
			}
		}

		public System.Web.UI.Control EditorControl
		{
			get { return _control; }
		}

	}
}
