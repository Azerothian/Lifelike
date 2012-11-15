using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using Lifelike.Data.Membership.Entities;
using System.Collections.Specialized;
using Lifelike.Data.Membership.Util;
using System.Web.Hosting;
using System.Web;
using System.Configuration.Provider;
using System.Configuration;
using System.Globalization;
using System.IO;
using Lifelike.Data.Membership.EntityLogic;

namespace Lifelike.Data.Membership
{
	/// <summary>
	/// 
	/// </summary>
	public class NHibernateProfileProvider : ProfileProvider
	{
		private string _applicationName;
		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
		///   
		/// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		///   
		/// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/> on a provider after the provider has already been initialized.</exception>
		public override void Initialize(string name, NameValueCollection config)
		{
			// Initialize values from Web.config.
			if (null == config)
			{
				throw (new ArgumentNullException("config"));
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "NHibernateProfileProvider";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", "NHibernate Profile Provider");
			}

			base.Initialize(name, config);
			_applicationName = ConfigurationUtil.GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);

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

		/// <summary>
		/// When overridden in a derived class, deletes all user-profile data for profiles in which the last activity date occurred before the specified date.
		/// </summary>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/>  value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <returns>
		/// The number of profiles deleted from the data source.
		/// </returns>
		public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int result = -1;
			using (var session = Database.Context.CurrentSession)
			{
				using (var tx = session.BeginTransaction())
				{
					result = ProfileLogic.DeleteInactiveProfiles(session, tx, userInactiveSinceDate);
					tx.Commit();
				}
			}

			return result;
		}

		/// <summary>
		/// When overridden in a derived class, deletes profile properties and information for profiles that match the supplied list of user names.
		/// </summary>
		/// <param name="usernames">A string array of user names for profiles to be deleted.</param>
		/// <returns>
		/// The number of profiles deleted from the data source.
		/// </returns>
		public override int DeleteProfiles(string[] usernames)
		{
			int result = 0;
			using (var session = Database.Context.CurrentSession)
			{
				using (var tx = session.BeginTransaction())
				{
					foreach (var uname in usernames)
					{
						User user = UserLogic.GetUser(session, uname);
						ProfileLogic.DeleteProfile(session, tx, user);
						result++;
					}
					tx.Commit();
				}
			}
			return result;
		}

		/// <summary>
		/// When overridden in a derived class, deletes profile properties and information for the supplied list of profiles.
		/// </summary>
		/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection"/>  of information about profiles that are to be deleted.</param>
		/// <returns>
		/// The number of profiles deleted from the data source.
		/// </returns>
		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			using (var session = Database.Context.CurrentSession)
			{
				using (var tx = session.BeginTransaction())
				{
					foreach (ProfileInfo prof in profiles)
					{

						User user = UserLogic.GetUser(session, prof.UserName);
						ProfileLogic.DeleteProfile(session, tx, user);

					}
					tx.Commit();
				}
				return profiles.Count;

			}
		}

		bool isAnonymous()
		{
			HttpContext current = HttpContext.Current;

			if (current != null)
			{
				if (current.Request.IsAuthenticated)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// When overridden in a derived class, retrieves profile information for profiles in which the last activity date occurred on or before the specified date and the user name matches the specified user name.
		/// </summary>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/> value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user profile information for inactive profiles where the user name matches the supplied <paramref name="usernameToMatch"/> parameter.
		/// </returns>
		public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			ProfileInfoCollection infos = new ProfileInfoCollection();
			try
			{
				using (var session = Database.Context.CurrentSession)
				{
					User u = UserLogic.GetUser(session, usernameToMatch);
					Profile prof = ProfileLogic.GetProfile(session, usernameToMatch);

					infos.Add(new ProfileInfo(u.Name, this.isAnonymous(), u.LastActivityDate, prof.LastActivityDate, prof.PropertyNames.Length + prof.PropertyValuesBinary.Length + prof.PropertyValuesString.Length));
					totalRecords = 1;
				}

			}
			catch (Exception ex)
			{
				throw new ProviderException("Unable to find Inactive Profiles By Username", ex);
			}
			return infos;
		}

		/// <summary>
		/// When overridden in a derived class, retrieves profile information for profiles in which the user name matches the specified user names.
		/// </summary>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user-profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch"/> parameter.
		/// </returns>
		public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, DateTime.Now, 0, 0, out totalRecords);
		}

		/// <summary>
		/// When overridden in a derived class, retrieves user-profile data from the data source for profiles in which the last activity date occurred on or before the specified date.
		/// </summary>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/>  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user-profile information about the inactive profiles.
		/// </returns>
		public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			ProfileInfoCollection infos = new ProfileInfoCollection();
			using (var session = Database.Context.CurrentSession)
			{
				IEnumerable<Profile> _profiles = ProfileLogic.GetProfileLastUpdatedBefore(session, userInactiveSinceDate, pageIndex, pageSize);

				totalRecords = _profiles.Count();
				foreach (Profile prof in _profiles)
				{
					User u = prof.User;
					infos.Add(new ProfileInfo(u.Name, this.isAnonymous(), u.LastActivityDate, prof.LastActivityDate, prof.PropertyNames.Length + prof.PropertyValuesBinary.Length + prof.PropertyValuesString.Length));
				}
			}
			return infos;
		}

		/// <summary>
		/// When overridden in a derived class, retrieves user profile data for all profiles in the data source.
		/// </summary>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="pageIndex">The index of the page of results to return.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of profiles.</param>
		/// <returns>
		/// A <see cref="T:System.Web.Profile.ProfileInfoCollection"/> containing user-profile information for all profiles in the data source.
		/// </returns>
		public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			using (var session = Database.Context.CurrentSession)
			{

				IEnumerable<Profile> _profiles = ProfileLogic.GetAllProfiles(session, pageIndex, pageSize, out totalRecords);
				ProfileInfoCollection infos = new ProfileInfoCollection();

				foreach (Profile prof in _profiles)
				{
					User u = prof.User;
					infos.Add(new ProfileInfo(u.Name, this.isAnonymous(), u.LastActivityDate, prof.LastActivityDate, prof.PropertyNames.Length + prof.PropertyValuesBinary.Length + prof.PropertyValuesString.Length));
				}
				return infos;
			}
		}

		/// <summary>
		/// When overridden in a derived class, returns the number of profiles in which the last activity date occurred on or before the specified date.
		/// </summary>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"/> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime"/> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"/>  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <returns>
		/// The number of profiles in which the last activity date occurred on or before the specified date.
		/// </returns>
		public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
						using (var session = Database.Context.CurrentSession)
			{
			return ProfileLogic.GetNumberOfInactiveProfiles(session,userInactiveSinceDate);
				}
		}

		/// <summary>
		/// Gets or sets the name of the currently running application.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that contains the application's shortened name, which does not contain a full path or extension, for example, SimpleAppSettings.</returns>
		public override string ApplicationName
		{
			get { return _applicationName; }
			set { _applicationName = value; }
		}

		private void ParseDataFromDB(string[] names, string values, byte[] buf, SettingsPropertyValueCollection properties)
		{
			if (((names != null) && (values != null)) && ((buf != null) && (properties != null)))
			{
				try
				{
					for (int i = 0; i < (names.Length / 4); i++)
					{
						string str = names[i * 4];
						SettingsPropertyValue value2 = properties[str];
						if (value2 != null)
						{
							int startIndex = int.Parse(names[(i * 4) + 2], CultureInfo.InvariantCulture);
							int length = int.Parse(names[(i * 4) + 3], CultureInfo.InvariantCulture);
							if ((length == -1) && !value2.Property.PropertyType.IsValueType)
							{
								value2.PropertyValue = null;
								value2.IsDirty = false;
								value2.Deserialized = true;
							}
							if (((names[(i * 4) + 1] == "S") && (startIndex >= 0)) && ((length > 0) && (values.Length >= (startIndex + length))))
							{
								value2.SerializedValue = values.Substring(startIndex, length);
							}
							if (((names[(i * 4) + 1] == "B") && (startIndex >= 0)) && ((length > 0) && (buf.Length >= (startIndex + length))))
							{
								byte[] dst = new byte[length];
								Buffer.BlockCopy(buf, startIndex, dst, 0, length);
								value2.SerializedValue = dst;
							}
						}
					}
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// Gets the property values.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="properties">The properties.</param>
		/// <returns></returns>
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
		{
			SettingsPropertyValueCollection svc = new SettingsPropertyValueCollection();

			if (properties.Count == 0)
				return svc;

			string[] names = null;
			string values = null;

			//Create the default structure of the properties
			foreach (SettingsProperty prop in properties)
			{
				if (prop.SerializeAs == SettingsSerializeAs.ProviderSpecific)
					if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string))
						prop.SerializeAs = SettingsSerializeAs.String;
					else
						prop.SerializeAs = SettingsSerializeAs.Xml;

				svc.Add(new SettingsPropertyValue(prop));
			}
						using (var session = Database.Context.CurrentSession)
			{
				
			Profile profile = ProfileLogic.GetProfile(session, (string)context["UserName"]);
			if (profile != null)
			{
				names = profile.PropertyNames.Split(':');
				values = profile.PropertyValuesString;

				if (names != null && names.Length > 0)
				{
					ParseDataFromDB(names, values, profile.PropertyValuesBinary, svc);
				}
			}

						}
			return svc;
		}

		/// <summary>
		/// Sets the property values.
		/// </summary>
		/// <param name="sc">The sc.</param>
		/// <param name="properties">The properties.</param>
		public override void SetPropertyValues(System.Configuration.SettingsContext sc, SettingsPropertyValueCollection properties)
		{

			string objValue = (string)sc["UserName"];
			bool userIsAuthenticated = (bool)sc["IsAuthenticated"];
			if (((objValue != null) && (objValue.Length >= 1)) && (properties.Count >= 1))
			{
				string allNames = string.Empty;
				string allValues = string.Empty;
				byte[] buf = null;
				PrepareDataForSaving(ref allNames, ref allValues, ref buf, true, properties, userIsAuthenticated);
				if (allNames.Length != 0)
				{
					using (var session = Database.Context.CurrentSession)
					{
						using (var tx = session.BeginTransaction())
						{
							Profile p = new Profile();
							p.User = UserLogic.GetUser(session, objValue);
							p.PropertyNames = allNames;
							p.PropertyValuesBinary = buf;
							p.PropertyValuesString = allValues;
							p.LastActivityDate = DateTime.Now;

							p.Save(session, tx);
						}
					}

				}
			}
		}

		void PrepareDataForSaving(ref string allNames, ref string allValues, ref byte[] buf, bool binarySupported, SettingsPropertyValueCollection properties, bool userIsAuthenticated)
		{
			StringBuilder builder = new StringBuilder();
			StringBuilder builder2 = new StringBuilder();
			MemoryStream stream = binarySupported ? new MemoryStream() : null;
			try
			{
				try
				{
					bool flag = false;
					foreach (SettingsPropertyValue value2 in properties)
					{
						if (value2.IsDirty && (userIsAuthenticated || ((bool)value2.Property.Attributes["AllowAnonymous"])))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return;
					}
					foreach (SettingsPropertyValue value3 in properties)
					{
						if ((!userIsAuthenticated && !((bool)value3.Property.Attributes["AllowAnonymous"])) || (!value3.IsDirty && value3.UsingDefaultValue))
						{
							continue;
						}
						int length = 0;
						int position = 0;
						string str = null;
						if (value3.Deserialized && (value3.PropertyValue == null))
						{
							length = -1;
						}
						else
						{
							object serializedValue = value3.SerializedValue;
							if (serializedValue == null)
							{
								length = -1;
							}
							else
							{
								if (!(serializedValue is string) && !binarySupported)
								{
									serializedValue = Convert.ToBase64String((byte[])serializedValue);
								}
								if (serializedValue is string)
								{
									str = (string)serializedValue;
									length = str.Length;
									position = builder2.Length;
								}
								else
								{
									byte[] buffer = (byte[])serializedValue;
									position = (int)stream.Position;
									stream.Write(buffer, 0, buffer.Length);
									stream.Position = position + buffer.Length;
									length = buffer.Length;
								}
							}
						}
						builder.Append(value3.Name + ":" + ((str != null) ? "S" : "B") + ":" + position.ToString(CultureInfo.InvariantCulture) + ":" + length.ToString(CultureInfo.InvariantCulture) + ":");
						if (str != null)
						{
							builder2.Append(str);
						}
					}
					if (binarySupported)
					{
						buf = stream.ToArray();
					}
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
			}
			catch
			{
				throw;
			}
			allNames = builder.ToString();
			allValues = builder2.ToString();
		}

	}
}
