using System.Xml;

namespace Discovery.Library.Protocol
{
	public sealed class HTTPProtocolResponse : XmlDocument, IProtocolResponse
	{
		#region IProtocolResponse Members

        public string MediaType
        {
            get 
            {
                var mediaTypeNode = this.SelectSingleNode("response/headers/header[@name='Content-Type']/mediaType");
                if (mediaTypeNode == null) return string.Empty;
                return mediaTypeNode.InnerText;
            }
        }

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
