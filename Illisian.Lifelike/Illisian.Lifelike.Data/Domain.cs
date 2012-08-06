using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Lifelike.Data
{
    public class Domain : Entity<Domain>
    {
        public virtual Item Item { get; set; }
        public virtual string BaseUri { get; set; }
        public virtual bool IsRegExMatch { get; set; }
        public virtual Language DefaultLanguage { get; set; }

    }
}
