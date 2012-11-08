using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Kernel.Entities
{
	public class Template : Entity<Template>
	{
		public virtual string Name { get; set; }
		public virtual string Value { get; set; }
	}

}

