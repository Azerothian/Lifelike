using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lifelike.Kernel.Util
{
	public class Serialisation
	{
		public static class Xml
		{

			/// <summary>
			/// Serializes the object.
			/// </summary>
			/// <param name="o">The o.</param>
			/// <returns></returns>
			public static string SerializeObject(Object o)
			{
				try
				{
					StringWriter stringWriter = new StringWriter();
					string result = "";
					var xs = new XmlSerializer(o.GetType());
					using (var xmlTextWriter = new StringWriter())
					{
						xs.Serialize(xmlTextWriter, o);
						result = xmlTextWriter.ToString();
					}
					return result;
				}
				catch (Exception e)
				{
					throw new Exception(String.Format("Lifelike.Kernel.Util.Serialisation.XMLSerializeObject(Object)"), e);
				}
			}


			/// <summary>
			/// Serializes the object.
			/// </summary>
			/// <param name="location">The location.</param>
			/// <param name="obj">The obj.</param>
			/// <returns></returns>
			public static bool SerializeObject(string location, Object obj)
			{
				try
				{
					var xs = new XmlSerializer(obj.GetType());
					using (var xmlTextWriter = new XmlTextWriter(location, Encoding.UTF8))
					{
						xs.Serialize(xmlTextWriter, obj);
						xmlTextWriter.Close();
					}
					return true;
				}
				catch (Exception e)
				{
					throw new Exception(String.Format("Lifelike.Kernel.Util.Serialisation.XMLSerializeObject('{0}', Object)", location), e);
				}
			}
			/// <summary>
			/// Deserializes the object.
			/// </summary>
			/// <param name="location">The location.</param>
			/// <param name="type">The type.</param>
			/// <returns></returns>
			public static Object DeserializeObject(string location, Type type)
			{
				try
				{
					var xs = new XmlSerializer(type);
					using (var xmlTextReader = new XmlTextReader(location))
					{
						return xs.Deserialize(xmlTextReader, "");
					}
				}
				catch (Exception e)
				{
					throw new Exception(String.Format("Lifelike.Kernel.Util.Serialisation.XMLDeserializeObject('{0}', '{1}')", location, type.ToString()), e);
				}

			}
			/// <summary>
			/// 
			/// </summary>
			public static class Generics
			{
				/// <summary>
				/// Serializes the object.
				/// </summary>
				/// <typeparam name="T"></typeparam>
				/// <param name="location">The location.</param>
				/// <param name="obj">The obj.</param>
				/// <returns></returns>
				public static bool SerializeObject<T>(string location, T obj)
				{
					try
					{
						var xs = new XmlSerializer(typeof(T));
						using (var xmlTextWriter = new XmlTextWriter(location, Encoding.UTF8))
						{
							xs.Serialize(xmlTextWriter, obj);
							xmlTextWriter.Close();
						}
						return true;
					}
					catch (Exception e)
					{
						throw new Exception(String.Format("Lifelike.Kernel.Util.Serialisation.XMLSerializeObject('{0}', Object)", location), e);
					}
				}
				/// <summary>
				/// Deserializes the object.
				/// </summary>
				/// <typeparam name="T"></typeparam>
				/// <param name="location">The location.</param>
				/// <returns></returns>
				public static T DeserializeObject<T>(string location)
				{
					try
					{
						var xs = new XmlSerializer(typeof(T));
						using (var xmlTextReader = new XmlTextReader(location))
						{
							return (T)xs.Deserialize(xmlTextReader);
						}
					}
					catch (Exception e)
					{
						throw new Exception(String.Format("Lifelike.Kernel.Util.Serialisation.XMLDeserializeObject('{0}', '{1}')", location, typeof(T).ToString()), e);
					}

				}

				public static T DeserializeObjectFromString<T>(string value)
				{
					try
					{
						StringReader data = new StringReader(value);
						var xs = new XmlSerializer(typeof(T));
						using (var xmlTextReader = new XmlTextReader(data))
						{
							return (T)xs.Deserialize(xmlTextReader);
						}
					}
					catch (Exception e)
					{
						throw new Exception(String.Format("Lifelike.Kernel.Util.Serialisation.XMLDeserializeObject('{0}', '{1}')", value, typeof(T).ToString()), e);
					}

				}

				/// <summary>
				/// Deserializes the object.
				/// </summary>
				/// <typeparam name="T"></typeparam>
				/// <param name="location">The location.</param>
				/// <returns></returns>
				public static T DeserializeObject<T>(Stream stream)
				{
					try
					{
						var xs = new XmlSerializer(typeof(T));
						using (var xmlTextReader = new XmlTextReader(stream))
						{
							return (T)xs.Deserialize(xmlTextReader);
						}
					}
					catch (Exception e)
					{
						throw new Exception(String.Format("Lifelike.Kernel.Util.Serialisation.XMLDeserializeObject('{0}')",  typeof(T).ToString()), e);
					}

				}
			}

		}
	}
}
