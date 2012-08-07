using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.Data;
using NHibernate;

namespace Illisian.Lifelike.Logic
{
    internal static class EntityExtension
    {
        internal static void Save<T>(this Entity<T> e, ISession session, ITransaction tx)
        {
            if (e.Id <= 0)
            {
                e.DateCreated = DateTime.Now;
                e.DateModified = DateTime.Now;
                session.Save(e);
            }
            else
            {
                e.DateModified = DateTime.Now;
                session.Update(e);
            }
        }
        internal static T Load<T>(this Entity<T> e, Guid g, ISession session, ITransaction tx)
        {
            T d = default(T);
            try
            {
                d = session.Load<T>(g);
            }
            catch (Exception ex)
            {
                throw new Exception("Error trying to load entity object", ex);
            }
            return (T)d;

        }
        internal static void Delete<T>(this Entity<T> e, ISession session, ITransaction tx)
        {
            try
            {
                session.Delete(e);
            }
            catch (Exception ex)
            {
                throw new Exception("Error trying to delete entity object", ex);
            }

        }
    }

}
