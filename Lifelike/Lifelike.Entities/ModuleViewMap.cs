using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
	public class ModuleViewMap: Entity<Module>
	{
		public virtual string Placeholder { get;set; }
		public virtual View View { get; set; }
		public virtual Module Module { get; set;}


	}
}
