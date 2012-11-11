using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Lifelike.Kernel.Entities
{
	public abstract class Entity<T> : IEntity
	{

		public virtual Guid Id { get; set; }
		public virtual bool Active { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual DateTime DateModified { get; set; }
		public virtual void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
		{
			model.Override<Entity<T>>(map =>
				map.Id(x => x.Id).GeneratedBy.Guid()
			);
		}
		public virtual void PreSave()
		{

		}
		public virtual void Save(ISession session, ITransaction tx)
		{
			this.PreSave();
			if (this.Id == Guid.Empty)
			{
				this.DateCreated = DateTime.Now;
				this.DateModified = DateTime.Now;
				session.Save(this);
			}
			else
			{
				this.DateModified = DateTime.Now;
				session.Update(this);
			}
		}

		public virtual void Refresh()
		{
			var session = Lifelike.Kernel.Database.Context.OpenSession();
			using (var tx = session.BeginTransaction())
			{
				Refresh(session);
			}
		}

		public virtual void Refresh(ISession session)
		{
			session.Refresh(this);
		}

		public virtual T Load(Guid g, ISession session)
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
		public virtual void Delete(ISession session, ITransaction tx)
		{
			try
			{
				session.Delete(this);
			}
			catch (Exception ex)
			{
				throw new Exception("Error trying to delete entity object", ex);
			}

		}
	}
}

