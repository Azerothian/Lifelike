using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Data.Entities
{
	public class Module : Entity<Module>
	{
		public virtual ISet<ModuleViewMap> Views { get; set; }
		public virtual string Name { get; set; }
		public virtual string Path { get; set; }
		//public virtual ICollection<Field> Fields { get; set; }
		
	}
}
