using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lifelike.Data.Membership.Entities;
using NHibernate;
using NHibernate.Linq;
namespace Lifelike.Data.Membership.EntityLogic
{
	public class ApplicationLogic : LogicAbstract<Application>
	{
		public static Application LoadOrCreate(ISession session, ITransaction tx, string name)
		{
			Application app = null;

			app = (from v in session.Query<Application>() where v.Active && v.Name == name select v).FirstOrDefault();
			if (app == null)
			{
				app = Create(session, tx, name);
			}
			return app;
		}

		public  static Application Create(ISession session, ITransaction tx, string name)
		{
			var app = new Application()
			{
				Name = name.ToUpper(),
				LoweredName = name.ToLower(),
				Active = true
			};
			app.Save(session, tx);
			return app;
		}

		public static Application GetApplication(ISession session, string name)
		{
			Application _app = null;
			using (var tx = session.BeginTransaction())
			{
				_app = ApplicationLogic.LoadOrCreate(session, tx, name);
				tx.Commit();
			}


			return _app;

		}
	}
}
