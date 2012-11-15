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
	public class RoleLogic : LogicAbstract<Role>
	{
		public static Role GetRole(ISession session, string roleName)
		{

			return LoadBy(session, v => (v.LoweredName == roleName.ToLower()));
		}

		public static bool RoleExists(ISession session, string roleName)
		{
			return GetRole(session, roleName) != null;
		}

		public static bool IsUserInRole(ISession session, Application app, User user, Role role)
		{

			return ApplicationUserRoleMapLogic.LoadAllBy(session, v => (v.Application == app &&
						v.Role == role && v.User == user)).Count() > 0;
		}
		public static IEnumerable<string> GetRoleNamesForUser(ISession session, Application app, User user)
		{
			return from v in user.ApplicationUserRoles
				   where
					   v.Application == app
				   select v.Role.Name;
		}

		public static void AddRoleToUser(ISession session, ITransaction tx, Application app, User user, Role role)
		{
			ApplicationUserRoleMap _newRole = new ApplicationUserRoleMap();
			_newRole.Application = app;
			_newRole.Role = role;
			_newRole.User = user;
			_newRole.Save(session, tx);
		}


		public static IEnumerable<string> GetUsernamesInRole(ISession session, Application app, Role role)
		{
			return from v in role.ApplicationUserRoles
				   where
					   v.Application == app
				   select v.User.LoweredName;
		}


		public static IEnumerable<string> GetAllRoleNames(ISession session, Application app)
		{
				return (from v in app.Roles
						select v.LoweredName);
		}


		public static IEnumerable<string> FindUsernamesInRole(ISession session, Application app,Role role, string usernameToMatch)
		{
			return from v in role.ApplicationUserRoles
				   where
					   v.Application == app
					   && v.User.LoweredName.Contains(usernameToMatch.ToLower())
				   select v.User.LoweredName;
		}
		public static void RemoveUserFromRole(ISession session, ITransaction tx, User user, Entities.Role role)
		{
			var r = (from v in user.ApplicationUserRoles
					 where v.Role == role
					 select v).FirstOrDefault();
			r.Delete(session, tx);

		}
	}
}
