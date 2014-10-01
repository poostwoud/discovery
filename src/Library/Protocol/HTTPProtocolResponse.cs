using System.Xml;

namespace Discovery.Library.Protocol
{
	public sealed class HTTPProtocolResponse : XmlDocument, IProtocolResponse
	{
		#region IProtocolResponse Members

		public string Result
		{
			get
			{
				return this.OuterXml;
			}
		}

		#endregion
	}
}
