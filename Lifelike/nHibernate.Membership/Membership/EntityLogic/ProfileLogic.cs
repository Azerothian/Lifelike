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
	public class ProfileLogic : LogicAbstract<Profile>
	{
		public static int DeleteInactiveProfiles(ISession session, ITransaction tx, DateTime userInactiveSinceDate)
		{
			int count = -1;

			var profiles = (from v in session.Query<Profile>()
							where v.Active
								&& v.LastActivityDate < userInactiveSinceDate
							select v);
			count = profiles.Count();
			foreach (var p in profiles)
			{
				p.Delete(session, tx);
			}

			return count;
		}
		public static void DeleteProfile(ISession session, ITransaction tx, User user)
		{

			DeleteAll(session, tx, p => (p.User == user));
		}

		public static Entities.Profile GetProfile(ISession session, string usernameToMatch)
		{
			return LoadBy(session, p => (p.User.Name == usernameToMatch));
		}

		public static IEnumerable<Profile> GetProfileLastUpdatedBefore(ISession session, DateTime userInactiveSinceDate, int pageIndex, int pageSize)
		{

			return LoadAllBy(session, v => (v.LastActivityDate < userInactiveSinceDate))
				.Skip(pageSize * pageIndex)
				.Take(pageSize);

		}

		public static IEnumerable<Profile> GetAllProfiles(ISession session, int pageIndex, int pageSize, out int totalRecords)
		{
			var _profiles = LoadAllBy(session);
			totalRecords = _profiles.Count();
			return _profiles.Skip(pageSize * pageIndex).Take(pageSize);
		}

		public static int GetNumberOfInactiveProfiles(ISession session, DateTime userInactiveSinceDate)
		{
			return LoadAllBy(session, v => (v.LastActivityDate < userInactiveSinceDate)).Count();
		}


	}
}
