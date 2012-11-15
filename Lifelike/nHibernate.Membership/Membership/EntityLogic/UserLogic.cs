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
	public class UserLogic : LogicAbstract<User>
	{
		/// <summary>
		/// Creates the user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="passwordFormat">The password format.</param>
		/// <param name="passwordSalt">The password salt.</param>
		/// <param name="email">The email.</param>
		/// <param name="passwordQuestion">The password question.</param>
		/// <param name="passwordAnswer">The password answer.</param>
		/// <param name="IsApproved">if set to <c>true</c> [is approved].</param>
		public static void CreateUser(ISession session, ITransaction tx, Application app, string username, string password, int passwordFormat, string passwordSalt, string email, string passwordQuestion, string passwordAnswer, bool IsApproved)
		{

			User user = new User();
			user.Active = true;
			user.Name = username;
			user.LoweredName = username.ToLower();
			user.Password = password;
			user.PasswordFormat = passwordFormat;
			user.PasswordSalt = passwordSalt;
			user.Email = email;
			user.LoweredEmail = email.ToLower();
			user.PasswordQuestion = passwordQuestion;
			user.PasswordAnswer = passwordAnswer;
			user.IsApproved = IsApproved;
			user.LastActivityDate = DateTime.Parse("1/1/1753 12:00:00 AM");
			user.LastLockedOutDate = DateTime.Parse("1/1/1753 12:00:00 AM");
			user.LastLoginDate = DateTime.Parse("1/1/1753 12:00:00 AM");
			user.LastPasswordChangeDate = DateTime.Parse("1/1/1753 12:00:00 AM");
			user.FailedPasswordAttemptWindowStart = DateTime.Parse("1/1/1753 12:00:00 AM");
			user.FailedPasswordAnswerAttemptWindowStart = DateTime.Parse("1/1/1753 12:00:00 AM");
			user.Applications.Add(app);
			user.Save(session, tx);
			//user.Applications = new NHibernate.Collection.Generic.PersistentGenericBag<Application>();

			// 
			// user.Save();
		}
		public static bool UserExists(ISession session, string username)
		{
			return GetUser(session, username) != null;
		}

		/// <summary>
		/// Gets the user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns></returns>
		public static User GetUser(ISession session, string username)
		{
			return LoadBy(session, v => (v.LoweredName == username.ToLower()));
		}

		public static void UpdateUser(ISession session, ITransaction tx, System.Web.Security.MembershipUser mu)
		{
			var user = GetUser(session, mu.UserName);
			user.FromMembershipUser(mu);
			user.Save(session, tx);
		}
		/// <summary>
		/// Gets the user by id.
		/// </summary>
		/// <param name="providerUserKey">The provider user key.</param>
		/// <returns></returns>
		public static User GetUserById(ISession session, object providerUserKey)
		{
			var Id = (Guid)(providerUserKey);

			return LoadBy(session, v => v.Id == Id);
		}


		/// <summary>
		/// Gets the user by email.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public static User GetUserByEmail(ISession session, string email)
		{
			return LoadBy(session, v => v.LoweredEmail == email.ToLower());
		}

		/// <summary>
		/// Gets all users online.
		/// </summary>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="compareTime">The compare time.</param>
		/// <returns></returns>
		public static IEnumerable<User> GetAllUsersOnline(ISession session, Application app, int pageIndex, int pageSize, DateTime compareTime)
		{

			return LoadAllBy(session, v => v.Applications.Where(p => p == app).Count() > 0
						 && v.LastActivityDate > compareTime).Skip(pageSize * pageIndex).Take(pageSize); ;

		}
		/// <summary>
		/// Gets the users online.
		/// </summary>
		/// <param name="compareTime">The compare time.</param>
		/// <returns></returns>
		public static int GetUsersOnline(ISession session, Application app, DateTime compareTime)
		{
			return LoadAllBy(session, v =>
					(v.Applications.Where(p => p == app).Count() > 0
						 && v.LastActivityDate > compareTime)).Count();

		}

		/// <summary>
		/// Finds the name of the users by.
		/// </summary>
		/// <param name="usernameToMatch">The username to match.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		public static IEnumerable<User> FindUsersByName(ISession session, Application app, string usernameToMatch, int pageIndex, int pageSize)
		{
			return LoadAllBy(session, v =>
				(v.Applications.Where(p => p == app).Count() > 0
						 && v.LoweredName.Contains(usernameToMatch.ToLower())));

		}
		/// <summary>
		/// Finds the users by email.
		/// </summary>
		/// <param name="emailToMatch">The email to match.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns></returns>
		public static IEnumerable<User> FindUsersByEmail(ISession session, Application app, string emailToMatch, int pageIndex, int pageSize)
		{
			return LoadAllBy(session, v =>
				(v.Applications.Where(p => p == app).Count() > 0
						 && v.LoweredEmail.Contains(emailToMatch.ToLower())));

		}

		/// <summary>
		/// Locks the user.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		public static void LockUser(ISession session, ITransaction tx, string userName)
		{
			var user = GetUser(session, userName);
			user.IsLockedOut = true;
			user.Save(session, tx);
		}

	}
}
