using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discovery.Library.Protocol;
using Discovery.Library.Format;

namespace Discovery.Library
{
	public sealed class Pipeline
	{
		public string TransformationsPath { get; private set; }
		public string UriConfiguration { get; private set; }

		public Pipeline(string transformationsPath, string uriConfiguration)
		{
			TransformationsPath = transformationsPath;
			UriConfiguration = uriConfiguration;
		}

		public IDictionary<string, IDocument> Process(Uri uri)
		{
			//*****
			var result = new Dictionary<string, IDocument>();
			
			//***** Step 1. Process the request for the protocol;
			//***** TODO:Out exception;
			var protocolResponse = Protocol.ProtocolHandler.Process(uri, UriConfiguration, new XmlMarshaller());
			result.Add("Raw", new Document { MediaType = "text/plain", Contents = protocolResponse.Raw });
			result.Add("Protocol", new Document { MediaType = "application/xml", Contents = protocolResponse.Result });

			//***** Step 2. Transform protocol response to Specify document;
			//***** TODO:Add media type signature detection in case of contenttype = generic;
			var specifyTransformation = System.IO.File.ReadAllText(string.Format(@"{0}{1}\{2}\specify.xslt", TransformationsPath, protocolResponse.Protocol, protocolResponse.MediaType));
			var specifyDocument = string.Empty;
			Exception exception;
			Transformation.XslHelper.Transform(protocolResponse.Result, specifyTransformation, out specifyDocument, out exception);
			result.Add("Specify", new Document { MediaType = "application/xml", Contents = specifyDocument});

			//***** Step 3. Transform specify document to display format;
			var displayTransformation = System.IO.File.ReadAllText(string.Format(@"{0}{1}\{2}\display.xslt", TransformationsPath, protocolResponse.Protocol, protocolResponse.MediaType));
			var displayDocument = string.Empty;
			Transformation.XslHelper.Transform(specifyDocument, displayTransformation, out displayDocument, out exception);
			result.Add("Display", new Document { MediaType = "text/html", Contents = displayDocument});

			//*****
			return result;
		}
	}
}
