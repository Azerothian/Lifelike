using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Kernel.Entities
{
	public class ItemViewMap : Entity<ItemViewMap>
	{
		public virtual View View { get; set; }
		public virtual Item Item { get; set;}


	}
}
