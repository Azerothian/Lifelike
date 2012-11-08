using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
	public class Layout : Entity<Layout>
	{
		public virtual string Name { get; set; }
		public virtual string Path { get; set; }
	}
}
