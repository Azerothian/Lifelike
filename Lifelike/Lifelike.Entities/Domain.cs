using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
    public class Domain : Entity<Domain>
    {
        public virtual Item StartItem { get; set; }
		public virtual Item BaseItem { get; set; }
        public virtual string BaseUri { get; set; }
        public virtual bool IsRegExMatch { get; set; }
        public virtual Language DefaultLanguage { get; set; }

    }
}
