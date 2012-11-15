using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lifelike.Data.Entities.Xml
{

	[Serializable]
	public class Property
	{
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Type { get; set; }
		[XmlText]
		public string Value { get; set; }
	}
}
