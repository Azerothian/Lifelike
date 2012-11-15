using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Lifelike.Data.Membership.Entities;
using System.Web.Configuration;
using System.Collections.Specialized;
using NHibernate;
using Lifelike.Data.Membership.Util;
using System.Web.Hosting;
using System.Configuration;
using System.Configuration.Provider;
using NHibernate.Type;
using System.Security.Cryptography;
using System.Reflection;
using Lifelike.Data.Membership.EntityLogic;

namespace Lifelike.Data.Membership
{

	/// <summary>
	/// Provides membership services using NHibernate as the persistence mechanism.
	/// </summary>
	public sealed class NHibernateMembershipProvider : MembershipProvider
	{
		#region Enumerations
		/// <summary>
		/// Types of failure that need to be tracked on a per user basis.
		/// </summary>
		private enum FailureType
		{
			Password,
			PasswordAnswer
		}
		#endregion

		#region Fields
		private string _applicationName;
		private bool _requiresQuestionAndAnswer;
		private bool _requiresUniqueEmail;
		private bool _enablePasswordRetrieval;
		private bool _enablePasswordReset;
		private int _maxInvalidPasswordAttempts;
		private int _passwordAttemptWindow;
		private MembershipPasswordFormat _passwordFormat;
		private int _minRequiredPasswordLength;
		private int _minRequiredNonAlphanumericCharacters;
		private string _passwordStrengthRegularExpression;
		private MachineKeySection _machineKey;
		#endregion Fields



		//private Lifelike.Data.Membership.Managers.ApplicationManager _applicationManager;
		//private UserManager _userManager;
		///// <summary>
		///// Gets the user manager.
		///// </summary>
		//public UserManager UserManager { get { return _userManager; } }
		#region Properties
		/// <summary>
		/// The name of the application using the custom membership provider.
		/// </summary>
		/// <returns>
		/// The name of the application using the custom membership provider.
		/// </returns>
		public override string ApplicationName
		{
			get { return _applicationName; }
			set { _applicationName = value; }
		}
		/// <summary>
		/// Gets a value indicating whether the membership provider is configured to require
		/// the user to answer a password question for password reset and retrieval.
		/// </summary>
		/// <returns>
		/// <c>true</c> if a password answer is required for password reset and retrieval;
		/// otherwise, <c>false</c>. The default is <c>true</c>.
		/// </returns>
		public override bool RequiresQuestionAndAnswer
		{
			get { return _requiresQuestionAndAnswer; }
		}
		/// <summary>
		/// Gets a value indicating whether the membership provider is configured to require
		/// a unique e-mail address for each user name.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the membership provider requires a unique e-mail address;
		/// otherwise, <c>false</c>. The default is <c>true</c>.
		/// </returns>
		public override bool RequiresUniqueEmail
		{
			get { return _requiresUniqueEmail; }
		}
		/// <summary>
		/// Indicates whether the membership provider is configured to allow users to retrieve
		/// their passwords.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the membership provider is configured to support password retrieval;
		/// otherwise, <c>false</c>. The default is <c>false</c>.
		/// </returns>
		public override bool EnablePasswordRetrieval
		{
			get { return _enablePasswordRetrieval; }
		}
		/// <summary>
		/// Indicates whether the membership provider is configured to allow users to reset their
		/// passwords.
		/// </summary>
		///<returns>
		///<c>true</c> if the membership provider supports password reset; otherwise, <c>false</c>.
		/// The default is <c>true</c>.
		///</returns>
		public override bool EnablePasswordReset
		{
			get { return _enablePasswordReset; }
		}
		/// <summary>
		/// Gets the number of invalid password or password-answer attempts allowed before the
		/// membership user is locked out.
		/// </summary>
		/// <returns>
		/// The number of invalid password or password-answer attempts allowed before the membership
		/// user is locked out.
		/// </returns>
		public override int MaxInvalidPasswordAttempts
		{
			get { return _maxInvalidPasswordAttempts; }
		}
		/// <summary>
		/// Gets the number of minutes in which a maximum number of invalid password or password-answer
		/// attempts are allowed before the membership user is locked out.
		/// </summary>
		/// <returns>
		/// The number of minutes in which a maximum number of invalid password or password-answer attempts
		/// are allowed before the membership user is locked out.
		/// </returns>
		public override int PasswordAttemptWindow
		{
			get { return _passwordAttemptWindow; }
		}
		/// <summary>
		/// Gets a value indicating the format for storing passwords in the data store.
		/// </summary>
		/// <returns>
		/// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"></see> values indicating
		/// the format for storing passwords in the data store.
		/// </returns>
		public override MembershipPasswordFormat PasswordFormat
		{
			get { return _passwordFormat; }
		}
		/// <summary>
		/// Gets the minimum length required for a password.
		/// </summary>
		/// <returns>
		/// The minimum length required for a password. 
		/// </returns>
		public override int MinRequiredPasswordLength
		{
			get { return _minRequiredPasswordLength; }
		}
		/// <summary>
		/// Gets the minimum number of special characters that must be present in a valid password.
		/// </summary>
		/// <returns>
		/// The minimum number of special characters that must be present in a valid password.
		/// </returns>
		public override int MinRequiredNonAlphanumericCharacters
		{
			get { return _minRequiredNonAlphanumericCharacters; }
		}
		/// <summary>
		/// Gets the regular expression used to evaluate a password.
		/// </summary>
		/// <returns>
		/// A regular expression used to evaluate a password.
		/// </returns>
		public override string PasswordStrengthRegularExpression
		{
			get { return _passwordStrengthRegularExpression; }
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
				name = "NHibernateMembershipProvider";
			}

			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", "NHibernate Membership Provider");
			}

			// Call the base class implementation.
			base.Initialize(name, config);

			// Load configuration data.

			//Illisian.Utilities.nHibernate.Manager.Context.AddAssembly(Assembly.GetExecutingAssembly(), "");
			//Illisian.Utilities.nHibernate.Manager.Context.AddTypes(new[] { typeof(User), typeof(Application), typeof(ApplicationUserRoleMap), typeof(Profile), typeof(Role) }, "");

			_applicationName = ConfigurationUtil.GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
			
			using (var session = Database.Context.CurrentSession)
			{
				ApplicationLogic.GetApplication(session, _applicationName);
			}
			_requiresQuestionAndAnswer =
				Convert.ToBoolean(ConfigurationUtil.GetConfigValue(config["requiresQuestionAndAnswer"], "False"));
			_requiresUniqueEmail = Convert.ToBoolean(ConfigurationUtil.GetConfigValue(config["requiresUniqueEmail"], "True"));
			_enablePasswordRetrieval = Convert.ToBoolean(ConfigurationUtil.GetConfigValue(config["enablePasswordRetrieval"], "True"));
			_enablePasswordReset = Convert.ToBoolean(ConfigurationUtil.GetConfigValue(config["enablePasswordReset"], "True"));
			_maxInvalidPasswordAttempts =
				Convert.ToInt32(ConfigurationUtil.GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
			_passwordAttemptWindow = Convert.ToInt32(ConfigurationUtil.GetConfigValue(config["passwordAttemptWindow"], "10"));
			_minRequiredPasswordLength = Convert.ToInt32(ConfigurationUtil.GetConfigValue(config["minRequiredPasswordLength"], "7"));
			_minRequiredNonAlphanumericCharacters =
				Convert.ToInt32(ConfigurationUtil.GetConfigValue(config["minRequiredAlphaNumericCharacters"], "1"));
			_passwordStrengthRegularExpression =
				Convert.ToString(ConfigurationUtil.GetConfigValue(config["passwordStrengthRegularExpression"], string.Empty));

			// Initialize the password format.
			switch (ConfigurationUtil.GetConfigValue(config["passwordFormat"], "Hashed"))
			{
				case "Hashed":
					_passwordFormat = MembershipPasswordFormat.Hashed;
					break;
				case "Encrypted":
					_passwordFormat = MembershipPasswordFormat.Encrypted;
					break;
				case "Clear":
					_passwordFormat = MembershipPasswordFormat.Clear;
					break;
				default:
					throw new System.Configuration.Provider.ProviderException("password format not supported");
			}

			// Get encryption and decryption key information from the configuration.
			Configuration cfg = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
			_machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");
			if ("Auto".Equals(_machineKey.Decryption))
			{
				// Create our own key if one has not been specified.
				_machineKey.DecryptionKey = KeyCreator.CreateKey(24);
				_machineKey.ValidationKey = KeyCreator.CreateKey(64);
			}
		}
		#endregion Initialization

		#region Operations

		#region User
		/// <summary>
		/// Adds a new membership user to the data source.
		/// </summary>
		/// <param name="username">the user name for the new user.</param>
		/// <param name="password">the password for the new user.</param>
		/// <param name="email">the e-mail address for the new user.</param>
		/// <param name="passwordQuestion">the password question for the new user.</param>
		/// <param name="passwordAnswer">the password answer for the new user.</param>
		/// <param name="isApproved">whether or not the new user is approved to be validated.</param>
		/// <param name="providerUserKey">the unique identifier from the membership data source for the user.</param>
		/// <param name="status">a <see cref="MembershipCreateStatus"/> enumeration value indicating whether the user was created successfully.</param>
		/// <returns>
		/// A <see cref="MembershipUser"/> object populated with the information for the newly created user.
		/// </returns>
		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
			string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Raise the ValidatingPassword event in case an event handler has been defined.
				ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
				OnValidatingPassword(args);
				if (args.Cancel)
				{
					status = MembershipCreateStatus.InvalidPassword;
					return null;
				}

				// Validate the e-mail address has not already been specified, if required.

				string un = null;
				try
				{
					un = GetUserNameByEmail(email);
				}
				catch (MemberAccessException) { }

				if (RequiresUniqueEmail && !string.IsNullOrEmpty(un))
				{
					status = MembershipCreateStatus.DuplicateEmail;
					return null;
				}

				using (var tx = session.BeginTransaction())
				{
					// Attempt to get the user record associated to the given user name.
					if (UserLogic.UserExists(session, username))
					{
						// Indicate we have found an existing user record.
						status = MembershipCreateStatus.DuplicateUserName;
					}
					else
					{
						// NOTE: providerUserKey is ignored on purpose. In this implementation it represents the user identifier.
						string encpassword = EncodePassword(password, _machineKey.ValidationKey);
						string encPasswordAnswer = EncodePassword(passwordAnswer, _machineKey.ValidationKey);

						try
						{
							UserLogic.CreateUser(session, tx, ApplicationLogic.GetApplication(session, _applicationName), username, encpassword, (int)PasswordFormat, _machineKey.ValidationKey, email, passwordQuestion, encPasswordAnswer, isApproved);
							status = MembershipCreateStatus.Success;
						}
						catch (Exception ex)
						{
							//status = MembershipCreateStatus.UserRejected;
							throw new ProviderException("Unable to create user", ex);
						}

						// Return the newly created user record.
						return GetUser(username, false);
					}

					// Indicate we were unable to create the user record.
					return null;
				}
			}
		}
		/// <summary>
		/// Removes a user from the data store.
		/// </summary>
		/// <param name="username">the name of the user to delete.</param>
		/// <param name="deleteAllRelatedData"><c>true</c> to delete data related to the user from the data store; <c>false</c> to leave related data intact.</param>
		/// <returns><c>true</c> if the user was successfully deleted; otherwise, <c>false</c>.</returns>
		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we are unable to perform the operation.
				bool result;

				// Delete the corresponding user record from the data store.
				try
				{
					// Get the user information.
					User user = UserLogic.GetUser(session, username);
					if (user != null)
					{
						// Process commands to delete all data for the user in the database.
						if (deleteAllRelatedData)
						{
							//// Delete the references to applications/users.
							//object[] values = new object[] { _application.Id, user.Id };
							//IType[] types = new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 };
							//_userManager.RemoveReferences("ApplicationUserRole.RemoveAppUserReferences", values, types);
						}
						// Delete the user record.
						using (var tx = session.BeginTransaction())
						{
							UserLogic.Delete(user, session, tx);
							tx.Commit();

						}
						result = true;
					}
					else
					{
						result = false;
					}


				}
				catch (Exception ex)
				{
					throw new MemberAccessException("Unable to delete user due to a Unknown Issue. Please See inner exception", ex);
				}

				// Return the result of the operation.
				return result;
			}
		}
		/// <summary>
		/// Updates information about a user in the data source.
		/// </summary>
		/// <param name="user">a <see cref="MembershipUser"/> object that represents the user to update and the updated information for the user.</param>
		public override void UpdateUser(MembershipUser user)
		{
			// Perform the update in the data store.
			try
			{
				using (var session = Database.Context.CurrentSession)
				{
					using (var tx = session.BeginTransaction())
					{
						UserLogic.UpdateUser(session, tx, user);
						tx.Commit();
					}
				}

			}
			catch (Exception ex)
			{
				throw new MemberAccessException("Unable to update user", ex);
			}
		}
		/// <summary>
		/// Clears a lock so that the membership user can be validated.
		/// </summary>
		/// <param name="username">the name of the user for whom to clear the lock status.</param>
		/// <returns><c>true</c> if the user was unlocked successfully; otherwise, <c>false</c>.</returns>
		public override bool UnlockUser(string username)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we are unable to perform the operation.
				bool result = false;

				// Unlock the user in the data store.
				try
				{
					// Get the user record form the data store.
					User user = UserLogic.GetUser(session, username);
					if (null != user)
					{
						using (var tx = session.BeginTransaction())
						{
							// Perform the update in the data store.
							user.IsLockedOut = false;
							user.LastLockedOutDate = DateTime.Now;
							user.LastActivityDate = DateTime.Now;
							user.Save(session, tx);
							result = true;
						}
					}
					else
					{
						throw new MemberAccessException("User was not found");
					}
				}
				catch (Exception ex)
				{
					throw new MemberAccessException("Unable to unlock user", ex);
				}

				// Return the result of the operation.
				return result;
			}
		}
		/// <summary>
		/// Verifies that the specified user name and password exist in the data source.
		/// </summary>
		/// <param name="username">the name of the user to validate.</param>
		/// <param name="password">The password for the specified user.</param>
		/// <returns><c>true</c> if the specified username and password are valid; otherwise, <c>false</c>.</returns>
		public override bool ValidateUser(string username, string password)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume the given user is not valid.
				bool isValid = false;

				// Get the password and the flag indicating the user is approved.
				User user = UserLogic.GetUser(session, username);
				if (null != user)
				{
					using (var tx = session.BeginTransaction())
					{
						// Ensure the passwords match but only if the user is not already locked out of the system.
						if (!user.IsLockedOut && CheckPassword(password, user.Password, user.PasswordSalt))
						{
							// Ensure the user is allowed to login.
							if (user.IsApproved)
							{
								// Indicate the user is valid.
								isValid = true;
								// Update the user's last login date.
								UpdateLastLoginDate(session, tx, username);
							}
						}
						else
						{
							// Update the failure count.
							UpdateFailureCount(session, tx, username, FailureType.Password);
						}
						tx.Commit();
					}
				}

				// Return the result of the operation.
				return isValid;
			}
		}
		/// <summary>
		/// Gets information from the data source for a user based on the unique identifier for the
		/// membership user. Provides an option to update the last-activity timestamp for the user.
		/// </summary>
		/// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
		/// <param name="userIsOnline"><c>true</c> to update the last-activity date/time stamp for the user; <c>false</c> to return user information without updating the last-activity date/time stamp for the user.</param>
		/// <returns>
		/// A <see cref="MembershipUser"></see> object populated with the specified user's information.
		/// </returns>
		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we were unable to find the user.
				MembershipUser user = null;

				// Ensure the provider key is valid.
				if (providerUserKey == null)
				{
					throw (new ArgumentNullException("providerUserKey"));
				}

				// Get the user record from the data store.
				try
				{
					user = UserLogic.GetUserById(session, providerUserKey).ToMembershipUser(Name);

				}
				catch (Exception ex)
				{
					throw new MemberAccessException("Unable to GetUserById", ex);
				}

				// Determine if we need to update the activity information.
				if (userIsOnline && (user != null))
				{
					using (var tx = session.BeginTransaction())
					{
						// Update the last activity timestamp (LastActivityDate).
						UpdateLastActivityDate(session, tx, user.UserName);
						tx.Commit();
					}
				}

				// Return the resulting user.
				return user;
			}
		}
		/// <summary>
		/// Gets information from the data source for a user. Provides an option to update the last-activity
		/// timestamp for the user.
		/// </summary>
		/// <param name="username">the name of the user for whom to get information.</param>
		/// <param name="userIsOnline"><c>true</c> to update the last-activity date/time stamp for the user; <c>false</c> to return user information without updating the last-activity date/time stamp for the user.</param>
		/// <returns>A <see cref="MembershipUser"/> object populated with the specified user's information.</returns>
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we were unable to find the user.
				MembershipUser user = null;

				// Don't accept empty user names.
				if (string.IsNullOrEmpty(username))
				{
					throw new ArgumentNullException("username");
				}

				// Get the user record from the data store.
				try
				{
					var r = UserLogic.GetUser(session, username);
					if (r != null)
						user = r.ToMembershipUser(Name);
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to GetUserByUsername", ex);
				}


				// Determine if we need to update the activity information.
				if (userIsOnline && (user != null))
				{
					// Update the last activity timestamp (LastActivityDate).
					using (var tx = session.BeginTransaction())
					{
						UpdateLastActivityDate(session, tx, user.UserName);
						tx.Commit();
					}
				}
				//if (user == null)
				//    throw new MemberAccessException("User was not found");

				// Return the resulting user.
				return user;
			}
		}
		/// <summary>
		/// Gets the user name associated with the specified e-mail address.
		/// </summary>
		/// <param name="email">the e-mail address to search for.</param>
		/// <returns>The user name associated with the specified e-mail address. If no match is found, return <c>null</c>.</returns>
		public override string GetUserNameByEmail(string email)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we were unable to find the corresponding user name.
				string username = null;

				// Don't accept empty emails.
				if (string.IsNullOrEmpty(email))
				{
					throw new ArgumentNullException("email");
				}

				// Get the user record from the data store.
				User user = null;
				try
				{
					user = UserLogic.GetUserByEmail(session, email);
				}
				catch (Exception ex)
				{
					throw new MemberAccessException("Unable to GetUserNameByEmail", ex);
				}
				if (user != null)
					username = user.LoweredName;
				//		else
				//			throw new MemberAccessException("Unable to GetUserNameByEmail. User was not found.");
				// Return the name of the user associated to the given e-mail address, if any.
				return username;
			}
		}
		/// <summary>
		/// Gets the number of users currently accessing the application.
		/// </summary>
		/// <returns>The number of users currently accessing the application.</returns>
		public override int GetNumberOfUsersOnline()
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume there are no users online.
				int numberOfUsersOnline;

				// Get a count of users whose LastActivityDate is greater than the threashold.
				try
				{
					// Determine the threashold based on the configured time window against which we'll compare.
					TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
					DateTime compareTime = DateTime.Now.Subtract(onlineSpan);


					numberOfUsersOnline = UserLogic.GetUsersOnline(session, ApplicationLogic.GetApplication(session, _applicationName), compareTime);


				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to GetNumberOfUsersOnline", ex);
				}

				// Return the result of the operation.
				return numberOfUsersOnline;
			}
		}
		/// <summary>
		/// Gets a collection of all the users in the data source in pages of data.
		/// </summary>
		/// <param name="pageIndex">the index of the page of results to return. <c>pageIndex</c> is zero-based.</param>
		/// <param name="pageSize">the size of the page of results to return.</param>
		/// <param name="totalRecords">the total number of matched users.</param>
		/// <returns>
		/// A <see cref="MembershipUserCollection"/> instance that contains a page of <c>pageSize</c> of
		/// <see cref="MembershipUser"/> objects beginning at the page specified by <c>pageIndex</c>.
		/// </returns>
		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Create a placeholder for all user accounts retrived, if any.
				MembershipUserCollection users = new MembershipUserCollection();

				// Get the user record from the data store.
				try
				{
					TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
					DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
					var page = UserLogic.GetAllUsersOnline(session, ApplicationLogic.GetApplication(session, _applicationName), pageIndex, pageSize, compareTime);

					foreach (User appUser in page.ToArray())
					{
						users.Add(appUser.ToMembershipUser(Name));
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to GetAllUsers", ex);
				}

				// Prepare return parameters.
				totalRecords = users.Count;

				// Return the result of the operation.
				return users;
			}
		}
		/// <summary>
		/// Gets a collection of membership users where the user name contains the specified user name to match.
		/// </summary>
		/// <param name="usernameToMatch">the user name to search for.</param>
		/// <param name="pageIndex">the index of the page of results to return. <c>pageIndex</c> is zero-based.</param>
		/// <param name="pageSize">the size of the page of results to return.</param>
		/// <param name="totalRecords">the total number of matched users.</param>
		/// <returns>
		/// A <see cref="MembershipUserCollection"/> instance that contains a page of <c>pageSize</c> of
		/// <see cref="MembershipUser"/> objects beginning at the page specified by <c>pageIndex</c>.
		/// </returns>
		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
			out int totalRecords)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Create a placeholder for all user accounts retrived, if any.
				MembershipUserCollection users = new MembershipUserCollection();

				// Get the user record from the data store.
				//try
				//{
				// Replace all * and ? wildcards for % and _, respectively.
				usernameToMatch = usernameToMatch.Replace('*', '%');
				usernameToMatch = usernameToMatch.Replace('?', '_');

				// Perform the search.

				var page = UserLogic.FindUsersByName(session, ApplicationLogic.GetApplication(session, _applicationName), usernameToMatch, pageIndex, pageSize);
				foreach (User appUser in page)
				{
					users.Add(appUser.ToMembershipUser(Name));
				}
				//}
				//catch (Exception ex)
				//{
				//    throw new ProviderException("Unable to FindUsersByName", ex);
				//}

				// Prepare return parameters.
				totalRecords = users.Count;

				// Return the result of the operation.
				return users;
			}
		}
		/// <summary>
		/// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
		/// </summary>
		/// <param name="emailToMatch">the e-mail address to search for.</param>
		/// <param name="pageIndex">the index of the page of results to return. <c>pageIndex</c> is zero-based.</param>
		/// <param name="pageSize">the size of the page of results to return.</param>
		/// <param name="totalRecords">the total number of matched users.</param>
		/// <returns>
		/// A <see cref="MembershipUserCollection"/> instance that contains a page of <c>pageSize</c> of
		/// <see cref="MembershipUser"/> objects beginning at the page specified by <c>pageIndex</c>.
		/// </returns>
		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
			out int totalRecords)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Create a placeholder for all user accounts retrived, if any.
				MembershipUserCollection users = new MembershipUserCollection();

				// Get the user record from the data store.
				try
				{
					// Replace all * and ? wildcards for % and _, respectively.
					emailToMatch = emailToMatch.Replace('*', '%');
					emailToMatch = emailToMatch.Replace('?', '_');
					// Perform the search.
					var page = UserLogic.FindUsersByEmail(session, ApplicationLogic.GetApplication(session, _applicationName), emailToMatch, pageIndex, pageSize);
					foreach (User appUser in page)
					{
						users.Add(appUser.ToMembershipUser(Name));
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to FindUsersByEmail", ex);
				}

				// Prepare return parameters.
				totalRecords = users.Count;

				// Return the result of the operation.
				return users;
			}
		}
		#endregion User

		#region Password
		/// <summary>
		/// Gets the password for the specified user name from the data store.
		/// </summary>
		/// <param name="username">the name of the user for whom to retrieve the password.</param>
		/// <param name="answer">the password answer for the user. </param>
		/// <returns>The password for the specified user name.</returns>
		public override string GetPassword(string username, string answer)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we are not able to fetch the user's password.
				string password = null;

				// Ensure password retrievals are allowed.
				if (!EnablePasswordRetrieval)
				{
					throw new MemberAccessException("Unable to Retrieve Password");
				}
				// Is the request made when the password in Hashed?
				if (MembershipPasswordFormat.Hashed == PasswordFormat)
				{
					throw new MemberAccessException("Unable to Retrieve Hashed Password");
				}

				// Get the user from the data store.
				User user = UserLogic.GetUser(session, username);
				if (null != user)
				{
					// Determine if the user is required to answer a password question.
					if (RequiresQuestionAndAnswer && !CheckPassword(answer, user.PasswordAnswer, user.PasswordSalt))
					{
						using (var tx = session.BeginTransaction())
						{
							UpdateFailureCount(session, tx, username, FailureType.PasswordAnswer);
							tx.Commit();
						}
						throw new MembershipPasswordException("Unable to Retrieve Password, Incorrect answer");

					}

					// Once the answer has been given, if required, determine if we need to unencode the password before
					// we return it. The call to UnencodePassword will just return the given password as is if the password
					// format is set to MembershipPasswordFormat.Clear.
					password = DecodePassword(user.Password);
				}

				// Return the retrieved password.
				return password;
			}
		}
		/// <summary>
		/// Processes a request to update the password for a membership user.
		/// </summary>
		/// <param name="newPassword">the new password for the specified user.</param>
		/// <param name="oldPassword">the current password for the specified user.</param>
		/// <param name="username">the name of the user for whom to update the password.</param>
		/// <returns><c>true</c> if the password was updated successfully; otherwise, <c>false</c>.</returns>
		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			using (var session = Database.Context.CurrentSession)
			{



				// Assume we are unable to perform the operation.
				bool result = false;

				// Ensure we are dealing with a valid user.
				if (ValidateUser(username, oldPassword))
				{
					// Raise the ValidatingPassword event in case an event handler has been defined.
					ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
					OnValidatingPassword(args);
					if (args.Cancel)
					{
						// Check for a specific error message.
						if (null != args.FailureInformation)
						{
							throw (args.FailureInformation);
						}
						else
						{
							throw new MembershipPasswordException("Change password cancelled due to a new password.");
						}
					}

					// Get the user from the data store.
					User user = UserLogic.GetUser(session, username);
					if (null != user)
					{
						try
						{
							using (var tx = session.BeginTransaction())
							{
								// Encode the new password.
								user.Password = EncodePassword(newPassword, user.PasswordSalt);
								user.LastPasswordChangeDate = DateTime.Now;
								user.LastActivityDate = DateTime.Now;
								// Update user record with the new password.

								user.Save(session, tx);
								tx.Commit();
							}
							// Indicate we successfully changed the password.
							result = true;
						}
						catch
						{
							throw new MembershipPasswordException("Change password cancelled due the account being locked.");
						}
					}
				}
				else
				{
					throw new MembershipPasswordException("Invalid Old Password");

				}

				// Return the result of the operation.
				return result;
			}
		}
		/// <summary>
		/// Processes a request to update the password question and answer for a membership user.
		/// </summary>
		/// <param name="newPasswordQuestion">the new password question for the specified user. </param>
		/// <param name="newPasswordAnswer">the new password answer for the specified user. </param>
		/// <param name="username">the name of the user for whom to update the password question and answer.</param>
		/// <param name="password">the password for the specified user. </param>
		/// <returns><c>true</c> if the password question and answer are updated successfully; otherwise, <c>false</c>.</returns>
		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
			string newPasswordAnswer)
		{
			using (var session = Database.Context.CurrentSession)
			{
				// Assume we are unable to perform the operation.
				bool result = false;

				// Ensure we are dealing with a valid user.
				if (ValidateUser(username, password))
				{
					// Get the user from the data store.
					User user = UserLogic.GetUser(session, username);
					if (null != user)
					{
						try
						{
							using (var tx = session.BeginTransaction())
							{
								// Update the new password question and answer.
								user.PasswordQuestion = newPasswordQuestion;
								user.PasswordAnswer = EncodePassword(newPasswordAnswer, user.PasswordSalt);
								user.LastActivityDate = DateTime.Now;
								// Update user record with the new password.
								user.Save(session, tx);
								// Indicate a successful operation.
								result = true;
								tx.Commit();
							}
						}
						catch
						{
							throw new ProviderException("Unable to change Password Question and Answer.");
						}
					}
				}

				// Return the result of the operation.
				return result;
			}
		}
		/// <summary>
		/// Resets a user's password to a new, automatically generated password.
		/// </summary>
		/// <param name="username">the name of the user for whom to reset the password.</param>
		/// <param name="answer">the password answer for the specified user. </param>
		/// <returns>The new password for the specified user.</returns>
		public override string ResetPassword(string username, string answer)
		{

			using (var session = Database.Context.CurrentSession)
			{
				using (var tx = session.BeginTransaction())
				{
					// Prepare a placeholder for the new passowrd.
					string newPassword;

					// Ensure password retrievals are allowed.
					if (!EnablePasswordReset)
					{
						throw new MembershipPasswordException("Password Reset not enabled.");
					}
					// Determine if a valid answer has been given if question and answer is required.
					if ((null == answer) && RequiresQuestionAndAnswer)
					{
						UpdateFailureCount(session, tx, username, FailureType.PasswordAnswer);
						throw new MembershipPasswordException("Password Answer required for a reset.");
					}

					// Generate a new random password of the specified length.
					newPassword = System.Web.Security.Membership.GeneratePassword(_minRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);

					// Raise the ValidatingPassword event in case an event handler has been defined.
					ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
					OnValidatingPassword(args);
					if (args.Cancel)
					{
						// Check for a specific error message.
						if (null != args.FailureInformation)
						{
							throw (args.FailureInformation);
						}
						else
						{
							throw new MembershipPasswordException("Unable to change Password Question and Answer.");
						}
					}

					// Get the user from the data store.
					User user = UserLogic.GetUser(session, username);
					if (null != user)
					{
						// Determine if the user is locked out of the system.
						if (user.IsLockedOut)
						{
							throw new MembershipPasswordException("User is Locked out.");
						}

						// Determine if the user is required to answer a password question.
						if (RequiresQuestionAndAnswer && !CheckPassword(answer, user.PasswordAnswer, user.PasswordSalt))
						{
							UpdateFailureCount(session, tx, username, FailureType.PasswordAnswer);
							throw new MembershipPasswordException("Incorrect Answer");
						}

						// Update user record with the new password.
						try
						{
							user.Password = EncodePassword(newPassword, user.PasswordSalt);
							user.LastPasswordChangeDate = DateTime.Now;
							user.LastActivityDate = DateTime.Now;
							user.Save(session, tx);
						}
						catch
						{
							throw new MembershipPasswordException("Operation Cancelled Due To Account Locked");
						}
					}
					tx.Commit();

					// Return the resulting new password.
					return newPassword;
				}
			}
		}
		#endregion Password

		#endregion Operations

		#region Helpers
		/// <summary>
		/// Update the login date for the given user name.
		/// </summary>
		/// <param name="username">name of the user for whom to update the last login date.</param>
		private void UpdateLastLoginDate(ISession session, ITransaction tx, string username)
		{
			// Get user record associated to the given user name.
			User user = UserLogic.GetUser(session, username);
			if (null != user)
			{
				// Update the last login timestamp (LastLoginDate).
				try
				{
					// Perform the update in the data store.
					user.LastLoginDate = DateTime.Now;
					user.Save(session, tx);
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to Update LastLoginDate", ex);
				}

			}

		}
		/// <summary>
		/// Update the activity date for the given user name.
		/// </summary>
		/// <param name="username">name of the user for whom to update the activity date.</param>
		private void UpdateLastActivityDate(ISession session, ITransaction tx, string username)
		{
			// Get user record associated to the given user name.
			User user = UserLogic.GetUser(session, username);
			if (null != user)
			{
				// Update the activity timestamp (LastActivityDate).
				try
				{

					// Perform the update in the data store.
					user.LastActivityDate = DateTime.Now;
					user.Save(session, tx);
					tx.Commit();

				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to Update LastActivityDate", ex);
				}

			}

		}
		/// <summary>
		/// A helper method that performs the checks and updates associated with password failure tracking.
		/// </summary>
		/// <param name="username">name of the user that is failing to specify a valid password.</param>
		/// <param name="failureType">type of failure to record.</param>
		private void UpdateFailureCount(ISession session, ITransaction tx, string username, FailureType failureType)
		{
			// Get user record associated to the given user name.
			User user = UserLogic.GetUser(session, username);
			if (null != user)
			{
				// Update the failure information for the given user in the data store.
				DateTime windowStart = DateTime.Now;
				int failureCount = 0;
				try
				{
					// First determine the type of update we need to do and get the relevant details.
					switch (failureType)
					{
						case FailureType.Password:
							windowStart = user.FailedPasswordAttemptWindowStart;
							failureCount = user.FailedPasswordAttemptCount;
							break;
						case FailureType.PasswordAnswer:
							windowStart = user.FailedPasswordAnswerAttemptWindowStart;
							failureCount = user.FailedPasswordAnswerAttemptCount;
							break;
					}

					// Then determine if the threashold has been exeeded.
					DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);
					if ((0 == failureCount) || DateTime.Now > windowEnd)
					{
						// First password failure or outside of window, start new password failure count from 1
						// and a new window start.
						switch (failureType)
						{
							case FailureType.Password:
								user.FailedPasswordAttemptWindowStart = DateTime.Now;
								user.FailedPasswordAttemptCount = 1;
								break;
							case FailureType.PasswordAnswer:
								user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
								user.FailedPasswordAnswerAttemptCount = 1;
								break;
						}
					}
					else
					{
						// Track failures.
						failureCount++;
						if (failureCount >= MaxInvalidPasswordAttempts)
						{
							// Password attempts have exceeded the failure threshold. Lock out the user.
							user.IsLockedOut = true;
							user.LastLockedOutDate = DateTime.Now;
						}
						else
						{
							switch (failureType)
							{
								case FailureType.Password:
									user.FailedPasswordAttemptCount = failureCount;
									break;
								case FailureType.PasswordAnswer:
									user.FailedPasswordAnswerAttemptCount = failureCount;
									break;
							}
						}
					}

					// Persist the changes.
					user.Save(session, tx);
				}
				catch (Exception ex)
				{
					throw new ProviderException("Unable to Update UpdateFailureCount", ex);
				}

			}
		}
		/// <summary>
		/// Compares password values based on the <see cref="MembershipPasswordFormat"/> property value.
		/// </summary>
		/// <param name="password1">first password to compare.</param>
		/// <param name="password2">second password to compare</param>
		/// <param name="validationKey">key to use when encoding the password.</param>
		/// <returns></returns>
		private bool CheckPassword(string password1, string password2, string validationKey)
		{
			// Format the password as required for comparison.
			switch (PasswordFormat)
			{
				case MembershipPasswordFormat.Hashed:
					password1 = EncodePassword(password1, validationKey);
					break;
				case MembershipPasswordFormat.Encrypted:
					password2 = DecodePassword(password2);
					break;
			}

			// Return the result of the comparison.
			return (password1 == password2);
		}
		/// <summary>
		/// Encrypts, Hashes, or leaves the password clear based on the <see cref="PasswordFormat"/> property value.
		/// </summary>
		/// <param name="password">the password to encode.</param>
		/// <param name="validationKey">key to use when encoding the password.</param>
		/// <returns>
		/// The encoded password only if all parameters are specified. If <c>validationKey</c> is <c>null</c>
		/// then the given <c>password</c> is returned untouched.
		/// </returns>
		private string EncodePassword(string password, string validationKey)
		{
			// Assume no encoding is performed.
			string encodedPassword = password;

			// Only perform the encoding if all parameters are passed and valid.
			if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(validationKey))
			{
				// Determine the type of encoding required.
				switch (PasswordFormat)
				{
					case MembershipPasswordFormat.Clear:
						// Nothing to do.
						break;
					case MembershipPasswordFormat.Encrypted:
						encodedPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
						break;
					case MembershipPasswordFormat.Hashed:
						// If we are not password a validation key, use the default specified.
						if (string.IsNullOrEmpty(validationKey))
						{
							// The machine key will either come from the Web.config file or it will be automatically generate
							// during initialization.
							validationKey = _machineKey.ValidationKey;
						}
						HMACSHA1 hash = new HMACSHA1();
						hash.Key = HexToByte(validationKey);
						encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
						break;
					default:

						throw new ProviderException("EncodePassword Unsupported Format");
				}
			}

			// Return the encoded password.
			return encodedPassword;
		}
		/// <summary>
		/// Decrypts or leaves the password clear based on the <see cref="PasswordFormat"/> property value.
		/// </summary>
		/// <param name="password">password to unencode.</param>
		/// <returns>Unencoded password.</returns>
		private string DecodePassword(string password)
		{
			// Assume no unencoding is performed.
			string unencodedPassword = password;

			// Determine the type of unencoding required.
			switch (PasswordFormat)
			{
				case MembershipPasswordFormat.Clear:
					// Nothing to do.
					break;
				case MembershipPasswordFormat.Encrypted:
					unencodedPassword = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(unencodedPassword)));
					break;
				case MembershipPasswordFormat.Hashed:
					throw new ProviderException("DecodePassword Unsupported Format");
				default:
					throw new ProviderException("DecodePassword Unsupported Format");
			}

			// Return the unencoded password.
			return unencodedPassword;
		}
		
		/// <summary>
		/// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration.
		/// </summary>
		/// <param name="hexString">hexadecimal string to conver.</param>
		/// <returns><c>byte</c> array containing the converted hexadecimal string contents.</returns>
		private static byte[] HexToByte(string hexString)
		{
			byte[] bytes = new byte[hexString.Length / 2 + 1];
			for (int i = 0; i <= hexString.Length / 2 - 1; i++)
			{
				bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
			}
			return bytes;
		}
		#endregion Helpers

	
	}
}