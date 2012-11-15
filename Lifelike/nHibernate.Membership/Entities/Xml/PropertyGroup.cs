using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lifelike.Data.Entities.Xml
{
	[Serializable]
	public class PropertyGroup
	{
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Type { get; set; }
		[XmlArray]
		[XmlArrayItem(ElementName = "Property", Type = typeof(Property))]
		public List<Property> Properties { get; set; }
	}
}
