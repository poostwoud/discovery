using System;
using Discovery.Library.Format;

namespace Discovery.Library.Protocol
{
	internal static class ProtocolHandler
	{
		/// <summary>
		/// Accepts an URI and handles the request using the defined protocol module.
		/// </summary>
		/// <param name="uri">The requested URI to process</param>
		/// <returns>IProtocolResponse</returns>
		internal static IProtocolResponse Process(Uri uri, string uriConfiguration = "", IFormatMarshaller formatMarshaller = null)
		{
			switch (uri.Scheme.ToLower())
			{
				case "http":
				case "https":
					return new HttpProtocol().Read(uri, uriConfiguration, formatMarshaller);
				default:
					throw new NotSupportedException(string.Format("Scheme {0} is not supported", uri.Scheme));
			}
		}
	}
}
