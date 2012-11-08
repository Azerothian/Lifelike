using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Kernel.Entities
{
	public class View : Entity<View>
	{
		public virtual string Name { get; set; }
		public virtual Layout Layout { get; set; }
		public virtual ISet<ModuleViewMap> Modules { get; set; }
	}
}

