using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Lifelike.Data
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual bool Active { get; set; }
        //public virtual DateTime DateCreated { get; set; }
        //public virtual DateTime DateModified { get; set; }


        public virtual bool ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            return true;
        }
    }
}
