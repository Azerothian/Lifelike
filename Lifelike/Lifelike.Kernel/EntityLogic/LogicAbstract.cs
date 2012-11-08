using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Kernel.Entities;
using NHibernate;
using NHibernate.Linq;
namespace Lifelike.Kernel.EntityLogic
{
	public abstract class LogicAbstract<T> where T : Entity<T>
	{

		public void Save(T l, ISession session, ITransaction tx)
		{
			l.DateModified = DateTime.Now;
			l.Save(session, tx);
		}

		public T LoadBy(ISession session, params Func<T, bool>[] paramArray)
		{
			return LoadAllBy(session, paramArray).FirstOrDefault();
		}
		public IEnumerable<T> LoadAllBy(ISession session, params Func<T, bool>[] paramArray)
		{
			IEnumerable<T> query = from v in session.Query<T>() where v.Active select v;

			foreach (var v in paramArray)
			{
				query = query.Where(v);
			}
			return query;

		}
		public void Delete(T l, ISession session, ITransaction tx)
		{
			l.Delete(session, tx);
		}

		public void SoftDelete(T l, ISession session, ITransaction tx)
		{
			l.Active = false;
			Save(l, session, tx);
		}
	}
}
