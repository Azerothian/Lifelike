using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.Data;
using NHibernate;
using NHibernate.Linq;
namespace Illisian.Lifelike.Logic.Data
{
	public abstract class LogicAbstract<T> where T : Entity<T>
	{

		public void Save(T l, ISession session, ITransaction tx)
		{
			l.DateModified = DateTime.Now;
			l.Save(session, tx);
		}

		public T LoadBy(ISession session, ITransaction tx, params Func<T, bool>[] paramArray)
		{
			return LoadAllBy(session, tx, paramArray).FirstOrDefault();
		}
		public IEnumerable<T> LoadAllBy(ISession session, ITransaction tx, params Func<T, bool>[] paramArray)
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
