using System;
using System.Xml;
using Discovery.Library.Format;

namespace Discovery.Library.Protocol
{
	public interface IProtocol
	{
		void Append();
		void Partial();
		IProtocolResponse Read(Uri uri, string uriConfiguration, IFormatMarshaller formatMarshaller, string userName = "", string password = "");
		void Remove();
		void Replace();
	}
}
