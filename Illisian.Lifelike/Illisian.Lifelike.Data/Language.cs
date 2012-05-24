using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections;

namespace Illisian.Lifelike.Data
{
    public class Language : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual ISet<Item> Items { get; set; }
        public virtual ISet<Domain> Domains { get; set; }

    }
}
