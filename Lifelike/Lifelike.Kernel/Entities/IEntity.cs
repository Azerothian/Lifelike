using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Kernel.Entities
{
    public interface IEntity
    {
        void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model);
    }
}
