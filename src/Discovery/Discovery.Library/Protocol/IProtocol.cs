using System;
using System.Xml;

namespace Discovery.Library.Protocol
{
	public interface IProtocol
	{
		void Append();
		void Partial();
		IProtocolResponse Read(Uri uri, string userName = "", string password = "");
		void Remove();
		void Replace();
	}
}
