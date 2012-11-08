using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Lifelike.Kernel.Fields
{
	public interface IField
	{

		Control EditorControl { get; }
		object Value { get; set; }
	}
}
