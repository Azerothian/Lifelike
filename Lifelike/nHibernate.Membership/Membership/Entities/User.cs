using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Lifelike.Data.Entities;
using NHibernate.Collection.Generic;

namespace Lifelike.Data.Membership.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class User : Entity<User>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
        public User()
            : base()
        {
            Applications = new HashSet<Application>();
			ApplicationUserRoles = new HashSet<ApplicationUserRoleMap>();
        }
		#region Properties
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets or sets the name of the lowered.
		/// </summary>
		/// <value>
		/// The name of the lowered.
		/// </value>
		public virtual string LoweredName { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public virtual string Description { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		public virtual string Password { get; set; }

		/// <summary>
		/// Gets or sets the password format.
		/// </summary>
		/// <value>
		/// The password format.
		/// </value>
		public virtual int PasswordFormat { get; set; }
		/// <summary>
		/// Gets or sets the password salt.
		/// </summary>
		/// <value>
		/// The password salt.
		/// </value>
		public virtual string PasswordSalt { get; set; }
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public virtual string Email { get; set; }
		/// <summary>
		/// Gets or sets the lowered email.
		/// </summary>
		/// <value>
		/// The lowered email.
		/// </value>
		public virtual string LoweredEmail { get; set; }
		/// <summary>
		/// Gets or sets the password question.
		/// </summary>
		/// <value>
		/// The password question.
		/// </value>
		public virtual string PasswordQuestion { get; set; }

		/// <summary>
		/// Gets or sets the password answer.
		/// </summary>
		/// <value>
		/// The password answer.
		/// </value>
		public virtual string PasswordAnswer { get; set; }
		/// <summary>
		/// Gets or sets the comments.
		/// </summary>
		/// <value>
		/// The comments.
		/// </value>
		public virtual string Comments { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is approved.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is approved; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsApproved { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is locked out.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is locked out; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsLockedOut { get; set; }
		/// <summary>
		/// Gets or sets the last activity date.
		/// </summary>
		/// <value>
		/// The last activity date.
		/// </value>
		public virtual DateTime LastActivityDate { get; set; }
		/// <summary>
		/// Gets or sets the last login date.
		/// </summary>
		/// <value>
		/// The last login date.
		/// </value>
		public virtual DateTime LastLoginDate { get; set; }
		/// <summary>
		/// Gets or sets the last locked out date.
		/// </summary>
		/// <value>
		/// The last locked out date.
		/// </value>
		public virtual DateTime LastLockedOutDate { get; set; }
		/// <summary>
		/// Gets or sets the last password change date.
		/// </summary>
		/// <value>
		/// The last password change date.
		/// </value>
		public virtual DateTime LastPasswordChangeDate { get; set; }

		/// <summary>
		/// Gets or sets the failed password attempt count.
		/// </summary>
		/// <value>
		/// The failed password attempt count.
		/// </value>
		public virtual int FailedPasswordAttemptCount { get; set; }

		/// <summary>
		/// Gets or sets the failed password attempt window start.
		/// </summary>
		/// <value>
		/// The failed password attempt window start.
		/// </value>
		public virtual DateTime FailedPasswordAttemptWindowStart { get; set; }

		/// <summary>
		/// Gets or sets the failed password answer attempt count.
		/// </summary>
		/// <value>
		/// The failed password answer attempt count.
		/// </value>
		public virtual int FailedPasswordAnswerAttemptCount { get; set; }

		/// <summary>
		/// Gets or sets the failed password answer attempt window start.
		/// </summary>
		/// <value>
		/// The failed password answer attempt window start.
		/// </value>
		public virtual DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

		/// <summary>
		/// Gets or sets the applications.
		/// </summary>
		/// <value>
		/// The applications.
		/// </value>
        public virtual ISet<Application> Applications { get; protected set; }


		/// <summary>
		/// Gets or sets the application user roles.
		/// </summary>
		/// <value>
		/// The application user roles.
		/// </value>
        public virtual ISet<ApplicationUserRoleMap> ApplicationUserRoles { get; set; }

		#endregion Properties

		#region Operations
		/// <summary>
		/// Toes the membership user.
		/// </summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <returns></returns>
		public virtual MembershipUser ToMembershipUser(string providerName)
		{
			return (new MembershipUser(providerName, Name, Id, Email, PasswordQuestion, Comments, IsApproved,
									   IsLockedOut, DateCreated, LastLoginDate, LastActivityDate, LastPasswordChangeDate,
									   LastLockedOutDate));
		}
		/// <summary>
		/// Froms the membership user.
		/// </summary>
		/// <param name="mu">The mu.</param>
		/// <returns></returns>
		public virtual User FromMembershipUser(MembershipUser mu)
		{
			Id = (Guid)(mu.ProviderUserKey);
			Name = mu.UserName;
			Email = mu.Email;
			PasswordQuestion = mu.PasswordQuestion;
			Comments = mu.Comment;
			IsApproved = mu.IsApproved;
			IsLockedOut = mu.IsLockedOut;
			DateCreated = mu.CreationDate;
			LastActivityDate = mu.LastActivityDate;
			LastLoginDate = mu.LastLoginDate;
			LastPasswordChangeDate = mu.LastPasswordChangedDate;
			LastLockedOutDate = mu.LastLockoutDate;
			return this;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
		#endregion Operations
	}
}
