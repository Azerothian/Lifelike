// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using PandoraJs.Utils.Extension;

namespace PandoraJs.Controls
{


	public class Form : Control
	{
		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<form id='" + ControlId + "'></form>");
		}
	}
}
