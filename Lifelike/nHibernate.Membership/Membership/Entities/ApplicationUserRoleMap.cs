using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Data.Entities;

namespace Lifelike.Data.Membership.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationUserRoleMap : Entity<ApplicationUserRoleMap>
	{
		/// <summary>
		/// Gets or sets the application.
		/// </summary>
		/// <value>
		/// The application.
		/// </value>
		public virtual Application Application { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		public virtual User User { get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
		public virtual Role Role { get; set; }
	}
}
