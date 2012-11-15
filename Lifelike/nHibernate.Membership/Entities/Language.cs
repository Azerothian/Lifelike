using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections;

namespace Lifelike.Kernel.Entities
{
    public class Language : Entity<Language>
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual ISet<Item> Items { get; set; }
        public virtual ISet<Domain> Domains { get; set; }
    }
}
