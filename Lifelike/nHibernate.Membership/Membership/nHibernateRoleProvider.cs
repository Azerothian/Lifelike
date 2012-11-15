using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Data.Membership.Entities;
using System.Web.Security;
using System.Collections.Specialized;
using Lifelike.Data.Membership.Util;
using System.Web.Hosting;
using System.Configuration.Provider;
using Lifelike.Data.Membership.EntityLogic;

namespace Lifelike.Data.Membership
{
	/// <summary>
	/// 
	/// </summary>
	public class NHibernateRoleProvider : RoleProvider
	{
		#region Fields
		private string _applicationName;
		#endregion Fields

		#region Properties
		/// <summary>
		/// The name of the application using the custom role provider.
		/// </summary>
		/// <returns>
		/// The name of the application using the custom role provider.
		/// </returns>
		public override string ApplicationName
		{
			get { return _applicationName; }
			set { _applicationName = value; }
		}
		#endregion Properties

		#region Initialization
		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <param name="config">A collection of the name/value pairs representing the provider-specific
		/// attributes specified in the configuration for this provider.</param>
		/// <param name="name">The friendly name of the provider.</param>
		/// <exception cref="ArgumentNullException">The name of the provider is null.</exception>
		/// <exception cref="InvalidOperationException">An attempt is made to call <see cref="Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
		/// <exception cref="ArgumentException">The name of the provider has a length of zero.</exception>
		public override void Initialize(string name, NameValueCollection config)
		{

			// Initialize values from Web.config.
			if (null == config)
			{
				throw (new ArgumentNullException("config"));
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "NHibernateRoleProvider";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", "NHibernate Role Provider");
			}

			// Call the base class implementation.
			base.Initialize(name, config);


			_applicationName = 	ConfigurationUtil.GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);

			// Load configuration data.
			using (var session = Database.Context.CurrentSession)
			{
				using (var tx = session.BeginTransaction())
				{

					ApplicationLogic.LoadOrCreate(session, tx, _applicationName);
					tx.Commit();
				}

			}
		}
		#endregion Initialization

		#region Operations
		/// <summary>
		/// Adds a new role to the data source for the configured application name.
		/// </summary>
		/// <param name="roleName">the name of the role to create.</param>
		public override void CreateRole(string roleName)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Make sure we are not attempting to insert an existing role.
				if (RoleExists(roleName))
				{
					throw new ProviderException("Role already Exists");
				}

				try
				{
					using (var tx = session.BeginTransaction())
					{
						Role role = new Role();
						role.Name = roleName;
						role.Applications.Add(ApplicationLogic.GetApplication(session, _applicationName));
						role.Save(session, tx);
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to create role.", ex);
				}
			}
		}
		/// <summary>
		/// Removes a role from the data source for the configured application name.
		/// </summary>
		/// <param name="roleName">the name of the role to delete.</param>
		/// <param name="throwOnPopulatedRole">if <c>true</c>, throw an exception if <c>roleName</c> has one or more
		/// members and do not delete <c>roleName</c>.</param>
		/// <returns><c>true</c> if the role was successfully deleted; otherwise, <c>false</c>.</returns>
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we are unable to perform the operation.
				bool result = false;

				// Check to see if we need to throw an exception if roles have been linked to other objects.
				if (throwOnPopulatedRole && (0 < GetUsersInRole(roleName).Length))
				{
					// Indicate the role is not empty and cannot be removed.
					throw new ProviderException("Role is not empty");
				}

				// Remove role information from the data store.
				try
				{
					// Get the role information.
					Role role = RoleLogic.GetRole(session, roleName);
					if (null != role)
					{
						// Delete the references to applications/roles.
						//object[] values = new object[] { application.Id, role.Id };
						//IType[] types = new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 };
						//NHibernateHelper.DeleteByNamedQuery("ApplicationUserRole.RemoveAppRoleReferences", values, types);
						//// Delete the role record.
						//NHibernateHelper.Delete(role);
						using (var tx = session.BeginTransaction())
						{
							RoleLogic.Delete(role, session, tx);
							tx.Commit();
						}
						// Indicate no errors occured.
						result = true;
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to delete role.", ex);
				}

				// Return the result of the operation.
				return result;
			}
		}
		/// <summary>
		/// Gets a value indicating whether the specified role name already exists in the data store
		/// for the configured application name.
		/// </summary>
		/// <param name="roleName">the name of the role to search for.</param>
		/// <returns>
		/// <c>true</c> if the role name already exists in the data store for the configured application name;
		/// otherwise, <c>false</c>.
		///</returns>
		public override bool RoleExists(string roleName)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume the role does not exist.
				bool exists;

				// Check against the data store if the role exists.
				try
				{
					exists = RoleLogic.RoleExists(session, roleName.ToLower());
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to check if role exists.", ex);
				}

				// Return the result of the operation.
				return exists;
			}
		}
		/// <summary>
		/// Gets a value indicating whether the specified user is in the specified role for the configured application name.
		/// </summary>
		/// <param name="username">the name of the user to search for.</param>
		/// <param name="roleName">the name of the role to search in.</param>
		/// <returns>
		/// <c>true</c> if the specified user is in the specified role for the configured application name;
		/// otherwise, <c>false</c>.
		/// </returns>
		public override bool IsUserInRole(string username, string roleName)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume the given role is not associated to the given user.
				bool isInRole = false;

				// Check against the data store if the role has been assigned to the given user.
				try
				{
					User user = UserLogic.GetUser(session,username);
					Role role = RoleLogic.GetRole(session , roleName);
					if ((null != user) && (null != role))
					{
						isInRole = RoleLogic.IsUserInRole(session, ApplicationLogic.GetApplication(session, _applicationName), user, role);
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to find user in role.", ex);
				}

				// Return the result of the operation.
				return isInRole;
			}
		}
		/// <summary>
		/// Gets a list of the roles that a specified user is in for the configured application name.
		/// </summary>
		/// <param name="username">the name of the user for whom to return a list of roles.</param>
		/// <returns>
		/// A string array containing the names of all the roles that the specified user is in for
		/// the configured application name.
		/// </returns>
		public override string[] GetRolesForUser(string username)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Prepare a placeholder for the roles.
				string[] roleNames = new string[0];

				// Load the list from the data store.
				try
				{
					User user = UserLogic.GetUser(session, username);
					roleNames = RoleLogic.GetRoleNamesForUser(session, ApplicationLogic.GetApplication(session, _applicationName), user).ToArray();

				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable get roles for user.", ex);
				}

				// Return the result of the operation.
				return roleNames;
			}
		}
		/// <summary>
		/// Adds the specified user names to the specified roles for the configured application name.
		/// </summary>
		/// <param name="roleNames">A string array of the role names to add the specified user names to. </param>
		/// <param name="userNames">A string array of user names to be added to the specified roles. </param>
		public override void AddUsersToRoles(string[] userNames, string[] roleNames)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Add the users to the given roles.
				try
				{
					// For every user in the given list attempt to add to the given roles.
					foreach (string userName in userNames)
					{
						// Assume that the given user name will be found. If any is not found this call will fail.
						User user = UserLogic.GetUser(session,userName);
						// Each role must be added from the user being currently processed. The assumption is that
						// the same list of roles will apply to all given users.
						foreach (string roleName in roleNames)
						{
							// Assume that the given user name will be found. If any is not found this call will fail.
							Role role = RoleLogic.GetRole( session, roleName);
							// NOTE: To ensure this relationship is stored we must use Save and not SaveOrUpdate.

							using (var tx = session.BeginTransaction())
							{
								RoleLogic.AddRoleToUser(session, tx, ApplicationLogic.GetApplication(session, _applicationName), user, role);
								tx.Commit();
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to add user to roles.", ex);
				}
			}
		}
		/// <summary>
		/// Removes the specified user names from the specified roles for the configured application name.
		/// </summary>
		/// <param name="userNames">string array of user names to be removed from the specified roles.</param>
		/// <param name="roleNames">string array of role names from which to remove the specified user names.</param>
		public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Remove the users from the given roles.
				try
				{
					// For every user in the given list attempt to remove the given roles.
					foreach (string userName in userNames)
					{
						// Assume that the given user name will be found. If any is not found this call will fail.
						User user = UserLogic.GetUser(session, userName);
						// Each role must be attempted to be removed from the user being currently processed. If no
						// association is found, ignore it.
						foreach (string roleName in roleNames)
						{
							// Assume that the given user name will be found. If any is not found this call will fail.
							Role role = RoleLogic.GetRole(session, roleName);
							// Execute the delete operation.
							using (var tx = session.BeginTransaction())
							{
								RoleLogic.RemoveUserFromRole(session, tx, user, role);
								tx.Commit();
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to remove user from roles.", ex);
				}
			}
		}
		/// <summary>
		/// Gets a list of users in the specified role for the configured application name.
		/// </summary>
		/// <param name="roleName">the name of the role for which to get the list of users.</param>
		/// <returns>
		/// A string array containing the names of all the users who are members of the specified role
		/// for the configured application name.
		/// </returns>
		public override string[] GetUsersInRole(string roleName)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Prepare a placeholder for the roles.
				string[] userNames = new string[0];

				// Load the list from the data store.
				try
				{
					Role role = RoleLogic.GetRole(session, roleName);
					userNames = RoleLogic.GetUsernamesInRole(session, ApplicationLogic.GetApplication(session, _applicationName), role).ToArray();
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to get users in role.", ex);
				}

				// Return the result of the operation.
				return userNames;
			}
		}
		/// <summary>
		/// Gets a list of all the roles for the configured application name.
		/// </summary>
		/// <returns>
		/// A string array containing the names of all the roles stored in the data store for the configured application name.
		/// </returns>
		public override string[] GetAllRoles()
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Prepare a placeholder for the roles.
				string[] roleNames = new string[0];

				// Load the list of roles for the configured application name.
				try
				{

					roleNames = RoleLogic.GetAllRoleNames(session, ApplicationLogic.GetApplication(session, _applicationName)).ToArray();
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to get all roles.", ex);
				}

				// Return the result of the operation.
				return roleNames;
			}
		}
		/// <summary>
		/// Gets an array of user names in a role where the user name contains the specified user name to match.
		/// </summary>
		/// <param name="roleName">the name of the role to search in.</param>
		/// <param name="usernameToMatch">the user name to search for.</param>
		/// <returns>
		/// A string array containing the names of all the users where the user name matches <c>usernameToMatch</c>
		/// and the user is a member of the specified role.
		/// </returns>
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Prepare a placeholder for the users.
				string[] userNames = new string[0];

				// Load the list of users for the given role name.
				try
				{

					// Replace all * and ? wildcards for % and _, respectively.
					usernameToMatch = usernameToMatch.Replace('*', '%');
					usernameToMatch = usernameToMatch.Replace('?', '_');

					// Perform the search.
					Role role = RoleLogic.GetRole(session, roleName);
					userNames = RoleLogic.FindUsernamesInRole(session, ApplicationLogic.GetApplication(session, _applicationName), role, usernameToMatch).ToArray();
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to find users in role.", ex);
				}

				// Return the result of the operation.
				return userNames;
			}
		}
		#endregion Operations
	}
}
