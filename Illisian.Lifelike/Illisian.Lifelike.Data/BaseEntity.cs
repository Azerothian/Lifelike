﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Lifelike.Data
{
    public abstract class BaseEntity
    {
        public virtual bool ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            return true;
        }
    }
}
