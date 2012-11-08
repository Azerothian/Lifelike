using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
	public class Field : Entity<Field>
	{
		public virtual FieldType Type { get; set; }
		public virtual string Value { get; set; }

	}
}
