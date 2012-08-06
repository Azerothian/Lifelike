using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Lifelike.Data
{
    public interface IEntity
    {
        bool ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model);
    }
}
