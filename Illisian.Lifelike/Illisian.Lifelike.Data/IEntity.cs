using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Lifelike.Data
{
    public interface IEntity
    {
        void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model);
    }
}
