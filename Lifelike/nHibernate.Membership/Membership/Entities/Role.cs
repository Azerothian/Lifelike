using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Data.Entities;
using NHibernate.Collection.Generic;

namespace Lifelike.Data.Membership.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class Role : Entity<Role>
	{
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
		/// Gets or sets the applications.
		/// </summary>
		/// <value>
		/// The applications.
		/// </value>

		public virtual ISet<Application> Applications { get; set; }

		/// <summary>
		/// Gets or sets the application user roles.
		/// </summary>
		/// <value>
		/// The application user roles.
		/// </value>
		public virtual ISet<ApplicationUserRoleMap> ApplicationUserRoles { get; set; }
	}
}
