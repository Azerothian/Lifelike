// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using PandoraJs.Utils.Extension;

namespace PandoraJs.Controls
{


	public class Panel : Control
	{
		protected override void Control_Render()
		{
			string filler = "";
			if (!HasChildren)
				filler = "&nbsp;";
			jQuery.Select("#" + Parent.ControlId).Append("<div id='" + ControlId + "'>" + filler + "</div>");
		}
	}
}
