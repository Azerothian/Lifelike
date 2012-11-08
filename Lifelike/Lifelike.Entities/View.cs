using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
	public class View : Entity<Item>
	{
		public virtual string Name { get; set; }
		public virtual Layout Layout { get; set; }
		public virtual ISet<ModuleViewMap> Modules { get; set; }
	}
}

