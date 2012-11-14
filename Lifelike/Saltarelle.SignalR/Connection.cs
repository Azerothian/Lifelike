using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Saltarelle.SignalR
{
	[IgnoreNamespace]
	[Imported(IsRealType = true)]
	public class Connection
	{
		[IntrinsicProperty]
		public dynamic client { get; set; }
		[IntrinsicProperty]
		public dynamic server { get; set; }
		[IntrinsicProperty]
		public Hub hub { get; set; }

	}
}
