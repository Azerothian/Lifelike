using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lifelike.Kernel.Entities.Xml
{
	[Serializable]
	public class TemplateData
	{
		[XmlArray]
		[XmlArrayItem(ElementName = "Property", Type = typeof(Property))]
		public List<Property> Properties { get; set; }
		[XmlArray]
		public List<PropertyGroup> PropertyGroups { get; set; }

		public Property this[string key]
		{
			get
			{
				return (from v in Properties where v.Name == key select v).FirstOrDefault();
			}
			set
			{

				if (value == null)
				{
					var rem = (from v in Properties where v.Name == key select v).FirstOrDefault();
					if (rem != null)
					{
						Properties.Remove(rem);
					}
					return;
				}
				

				var prop = (from v in Properties where v.Name == key select v).FirstOrDefault();

				if (prop != null)
				{
					prop.Name = key;
					prop.Type = value.Type;
					prop.Value = value.Value;
				}
				else
				{
					Properties.Add(value);
				}
			}
		}

	}
}
