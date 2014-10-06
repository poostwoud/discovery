using System.Xml;

namespace Discovery.Library.Extensions
{
	public static class XMLExtensions
	{
		public static XmlElement AppendElement(this XmlDocument xmlDocument, string name, string value = null, XmlElement parent = null)
		{
			var element = xmlDocument.CreateElement(name);
			if (value != null) element.InnerText = value;
			if (parent == null)
				xmlDocument.AppendChild(element);
			else
				parent.AppendChild(element);
			return element;
		}

		public static XmlElement AppendElement(this XmlElement xmlElement, string name, string value = null, bool raw = false)
		{
			var element = xmlElement.OwnerDocument.CreateElement(name);
			if (value != null)
				if (raw)
					element.InnerXml = value;
				else
					element.InnerText = value;
			xmlElement.AppendChild(element);
			return element;
		}

		public static XmlAttribute AppendAttribute(this XmlElement xmlElement, string name, string value = null)
		{
			var attribute = xmlElement.OwnerDocument.CreateAttribute(name);
			if (value != null) attribute.Value = value;
			xmlElement.Attributes.Append(attribute);
			return attribute;
		}
	}
}
