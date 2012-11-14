using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.JScript.Admin
{
	public class Guid
	{
		public static string CreateGuid()
		{
			return (
		   S4() + S4() + "-" +
		   S4() + "-" +
		   S4() + "-" +
		   S4() + "-" +
		   S4() + S4() + S4()
	   );
		}
		private static string S4()
		{
			var r = Math.Floor(
				Math.Random() * 0x10000 /* 65536 */
			);
			return r.ToString().Substr(0, 4);
		}

	}
}
