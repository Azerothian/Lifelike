using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
    public abstract class Entity<T> : IEntity
    {
		
        public virtual Guid Id { get; set; }
        public virtual bool Active { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
        public virtual void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<Entity<T>>(map =>
                map.Id(x => x.Id).GeneratedBy.Guid()
            );
        }


	}
}

