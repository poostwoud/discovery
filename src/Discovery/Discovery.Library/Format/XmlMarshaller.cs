using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace Discovery.Library.Format
{
	internal sealed class XmlMarshaller : IFormatMarshaller
	{
		/// <summary>
		/// Converts JSON document to XML Document.
		/// </summary>
		/// <param name="document">JSON string</param>
		/// <returns>XML string</returns>
		private string Json(string document)
		{
			XmlDocument xml = new XmlDocument();
			using (var memoryStream = new MemoryStream())
			{
				var streamWriter = new StreamWriter(memoryStream);
				streamWriter.Write(document);
				streamWriter.Flush();
				memoryStream.Position = 0;
				using (XmlDictionaryReader xmlReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, XmlDictionaryReaderQuotas.Max))
				{
					xmlReader.Read();
					xml.LoadXml(xmlReader.ReadOuterXml());
				}
				streamWriter.Close();
			}
			return xml.OuterXml;
		}	

		#region IFormatMarshaller Members

		public string Marshal(string formatType, string document)
		{
			var formatTypeLowered = formatType.ToLower();
			if (formatTypeLowered.EndsWith("json"))
					return Json(document);
			else
				throw new NotSupportedException(string.Format("Format type {0} is not supported", formatType));
		}

		#endregion
	}
}
