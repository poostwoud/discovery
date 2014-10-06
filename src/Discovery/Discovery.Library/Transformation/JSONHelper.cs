using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

/// <summary>
/// Static class containing helper methods to work with JSON objects.
/// Source: http://ashwinjp.wordpress.com/2011/10/11/convert-json-objects-to-xml/
/// </summary>
namespace Discovery.Library.Transformation
{
	public static class JSONHelper
	{
		/// <summary>
		/// Converts JSON object to XML Document.
		/// </summary>
		/// <param name="stream">IO stream containing the JSON object.</param>
		/// <returns></returns>
		public static XmlDocument ConvertToXMLDocument(Stream stream)
		{
			XmlDocument document = new XmlDocument();
			using (XmlDictionaryReader xmlReader = JsonReaderWriterFactory.CreateJsonReader(stream, XmlDictionaryReaderQuotas.Max))
			{
				xmlReader.Read();
				document.LoadXml(xmlReader.ReadOuterXml());
			}
			return document;
		}

		/// <summary>
		/// Converts JSON object to XML Document.
		/// </summary>
		/// <param name="JSONString">JSON string.</param>
		/// <returns></returns>
		public static XmlDocument ConvertToXMLDocument(string JSONString)
		{
			XmlDocument document = new XmlDocument();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				streamWriter.Write(JSONString);
				streamWriter.Flush();
				memoryStream.Position = 0;
				using (XmlDictionaryReader xmlReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, XmlDictionaryReaderQuotas.Max))
				{
					xmlReader.Read();
					document.LoadXml(xmlReader.ReadOuterXml());
				}
				streamWriter.Close();
			}
			return document;
		}

		public static string ConvertToXML(string JSONString)
		{
			return ConvertToXMLDocument(JSONString).OuterXml;
		}
	}
}
