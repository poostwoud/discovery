using System.Xml;

namespace Discovery.Library.Protocol
{
	public sealed class HttpProtocolResponse : XmlDocument, IProtocolResponse
	{
		#region IProtocolResponse Members

		public string Protocol
		{
			//***** TODO:Retrieve from xml;
			get { return "http"; } 
		}

    public string MediaType { get; internal set; }

		public string Result
		{
			get
			{
				return this.OuterXml;
			}
		}

    public string Raw { get; internal set; }

		#endregion
  }
}
