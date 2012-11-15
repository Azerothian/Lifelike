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
	public class Profile : Entity<Profile>
	{
        /// <summary>
        /// Gets or sets the property values binary.
        /// </summary>
        /// <value>
        /// The property values binary.
        /// </value>
		public virtual byte[] PropertyValuesBinary { get; set; }

        /// <summary>
        /// Gets or sets the property names.
        /// </summary>
        /// <value>
        /// The property names.
        /// </value>
		public virtual string PropertyNames { get; set; }

        /// <summary>
        /// Gets or sets the property values string.
        /// </summary>
        /// <value>
        /// The property values string.
        /// </value>
		public virtual string PropertyValuesString { get; set; }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        /// <value>
        /// The last activity date.
        /// </value>
		public virtual DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
		public virtual User User { get; set; }

	}
}
