using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Lifelike.Kernel;
using NHibernate.Linq;
using Lifelike.Entities;

namespace Lifelike.Kernel.EntityLogic
{
    public class LanguageLogic :  LogicAbstract<Language>
    {
        public Language Create(string name, string code, ISession session, ITransaction tx)
        {
            Language l = new Language()
            {
                Active = true,
                Code = code,
                Name = name
            };
            Save(l, session, tx);
            return l;
        }

    }
}
