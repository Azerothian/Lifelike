using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.Data;
using NHibernate;
using Illisian.Lifelike.Logic;
using NHibernate.Linq;

namespace Illisian.Lifelike.Logic.Data
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
