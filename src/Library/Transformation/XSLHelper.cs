using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Discovery.Library.Transformation
{
	public static class XSLHelper
	{
		/// <summary>
		/// Transforms a XML file to another XML file using a XSL transformation.
		/// </summary>
		/// <remarks>
		/// Source: http://www.csharpfriends.com/Articles/getArticle.aspx?articleID=63
		/// </remarks>
		/// <param name="xmlInputFullFileName"></param>
		/// <param name="xslFullFileName"></param>
		/// <param name="xmlOutputFullFileName"></param>
		/// <param name="exception"></param>
		public static bool Transform(string xmlInputFullFileName, string xslFullFileName, string xmlOutputFullFileName, out Exception exception)
		{
			//***** Initialize;
			var result = false;
			exception = null;

			//*****
			try
			{
				//***** Load the XML document using the specified file name;
				var xPathDocument = new XPathDocument(xmlInputFullFileName);

				//***** Load the XSL transformation using the specified file name;
				var xslCompiledTransform = new XslCompiledTransform();
				xslCompiledTransform.Load(xslFullFileName);

				//***** Create output file stream using Utf-8 encoding;
				using (var xmlTextWriter = new XmlTextWriter(xmlOutputFullFileName, null))
				{
					//***** Transform the XML;
					xslCompiledTransform.Transform(xPathDocument, null, xmlTextWriter);
					xmlTextWriter.Close();
				}

				//*****
				result = true;
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			//*****
			return result;
		}
	}
}
