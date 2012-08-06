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
    class LanguageLogic
    {
        public Language Create(string name, string code, ISession session, ITransaction tx)
        {
            Language l = new Language()
            {
                Active = true,
                Code = code,
                Name = name
            };
            l.Save(session, tx);
            return l;
        }
        public void Save(Language l, ISession session, ITransaction tx)
        {
            l.Save(session, tx);
        }

        public Language LoadBy(ISession session, ITransaction tx, params Func<Language, bool>[] paramArray)
        {
            
            IEnumerable<Language> query = session.Query<Language>();
            foreach (var v in paramArray)
            {
                query = query.Where(v);
            }
            return query.FirstOrDefault();

        }
        public void Delete(Language l, ISession session, ITransaction tx)
        {
            l.Delete(session, tx);
        }
    }
}
