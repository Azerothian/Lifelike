﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
	public class FieldType : Entity<FieldType>
	{
		public virtual string Name { get; set; }
	}
}