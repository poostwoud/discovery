using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Discovery.Library.Extensions;
using Discovery.Library.Format;

namespace Discovery.Library.Protocol
{
	public sealed class HttpProtocol : IProtocol
    {
        #region IProtocol Members

        public void Append()
		{
			throw new NotImplementedException();
		}

		public void Partial()
		{
			throw new NotImplementedException();
		}

		public IProtocolResponse Read(Uri uri, string uriConfiguration, IFormatMarshaller formatMarshaller = null, string userName = "", string password = "")
		{
			//*****
			var document = new HttpProtocolResponse();

			//*****
			var responseElement = document.AppendElement("response");

			//*****
			try
			{
				//*****
				var request = (HttpWebRequest)WebRequest.Create(uri);

				//***** Test for uri configuration;
				//***** TODO:Factory + data format;
				var allConfig = new XmlDocument();
				allConfig.Load(uriConfiguration);
				
				var uriConfigNode = allConfig.SelectSingleNode(string.Format("apis/api[@uri='{0}']", uri));
				var responseMediaTypeOverride = "";
				if (uriConfigNode != null)
				{
					//***** Response media type override;
					var mediaTypeNode = uriConfigNode.SelectSingleNode("mediaType");
					if (mediaTypeNode != null)
						responseMediaTypeOverride = mediaTypeNode.InnerText;

					//***** Headers;
					var headers = uriConfigNode.SelectNodes("headers/header");
					foreach (XmlNode header in headers)
					{
						var headerName = header.Attributes["name"].InnerText;
						var headerValue = header.InnerText;
						switch (headerName.ToLower())
						{
							case "accept":
								request.Accept = headerValue;
								break;
							default:
								request.Headers.Add(headerName, headerValue);
								break;
						}
					}
				}

				//*****
				request.Method = "GET";

				//***** Force Basic Authorization for now (assume empty password is allowed);
				if (!string.IsNullOrWhiteSpace(userName))
					request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(userName + ":" + password));

				//***** TODO:Allow to pass UserAgent? Most APIs dont seem to allow "agentless" calls;
				request.UserAgent = "Discovery";

				/*request.Credentials = new NetworkCredential(userName, password);
				request.PreAuthenticate = true;*/
				
				//*****
				string entityBody;

				//***** Get response;
				using (var response = (HttpWebResponse) request.GetResponse())
				{
					#region Status Line

					//*****
					var statusLineElement = responseElement.AppendElement("statusLine");

					//*****
					statusLineElement.AppendElement("protocolName", "HTTP");
					statusLineElement.AppendElement("protocolVersion", response.ProtocolVersion.ToString());
					statusLineElement.AppendElement("statusCode", Convert.ToInt32(response.StatusCode).ToString());
					statusLineElement.AppendElement("statusDescription", response.StatusDescription);

					#endregion //***** Status Line;

					#region Headers

					var headersElement = responseElement.AppendElement("headers");
					var headers = response.Headers;
					foreach (var key in headers.AllKeys)
					{
						//*****
						var headerElement = headersElement.AppendElement("header");
						headerElement.AppendAttribute("name", key);

						//***** Handle specifics based on RFC2616;
						switch (key.ToLower())
						{
							case "content-type":
								var contentTypeValues = headers.GetValues(key);
								var contentTypeValue = contentTypeValues[0];
								var contentTypeParts = contentTypeValue.Split(new[]{";"}, StringSplitOptions.RemoveEmptyEntries);
								headerElement.AppendElement("mediaType", contentTypeParts[0].Trim());
								if (contentTypeParts.Length == 2)
								{
									var contentEncodingParts = contentTypeParts[1].Split(new[]{"="}, StringSplitOptions.RemoveEmptyEntries);
									headerElement.AppendElement(contentEncodingParts[0].Trim(), contentEncodingParts[1].Trim());
								}
								break;
							default:
								foreach (var value in headers.GetValues(key))
									headerElement.AppendElement("value", value);
								break;
						}
					}

					#endregion //***** Headers

					#region Entity

					//*****
					var entityElement = responseElement.AppendElement("entity");
					entityElement.AppendElement("encoding", response.ContentEncoding);
					entityElement.AppendElement("type", response.ContentType);

					//***** Get response body;
					//***** NOTE:RFC-2616:There is no default encoding => Don't apply, use StreamReader auto determine;
					var encodingName = response.ContentEncoding;
					if (string.IsNullOrWhiteSpace(encodingName))
					{
						var charsetNode = document.SelectSingleNode("response/headers/header[@name='Content-Type']/charset");
						if (charsetNode != null)
							encodingName = charsetNode.InnerText;
					}

					//*****
					if (string.IsNullOrWhiteSpace(encodingName))
						using (var bodyReader = new StreamReader(response.GetResponseStream()))
							entityBody = bodyReader.ReadToEnd();
					else
						using (var bodyReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encodingName)))
							entityBody = bodyReader.ReadToEnd();

					//***** TODO:Override only when media type is format type?
					var mediaType = responseMediaTypeOverride;
					if (string.IsNullOrWhiteSpace(mediaType))
					{
						var mediaTypeNode = document.SelectSingleNode("response/headers/header[@name='Content-Type']/mediaType");
						if (mediaTypeNode != null && string.IsNullOrWhiteSpace(mediaType))
							mediaType = mediaTypeNode.InnerText.ToLower();
						/*else if (mediaTypeNode == null && !string.IsNullOrWhiteSpace(responseMediaTypeOverride))
							mediaType = responseMediaTypeOverride;*/
					}

					//*****
					document.MediaType = mediaType;

					//*****
					if (string.IsNullOrWhiteSpace(mediaType))
						entityElement.AppendElement("body", entityBody);
					else
					{
						var marshalledEntityBody = formatMarshaller.Marshal(mediaType, entityBody);
						entityElement.AppendElement("body", marshalledEntityBody, true);
					}

					//***** When the content length is not provided
					entityElement.AppendElement("length", response.ContentLength.ToString());

					#endregion //***** Entity

          #region Raw

          document.Raw = string.Format("HTTP/{0} {1} {2}\r\n{3}{4}", response.ProtocolVersion, Convert.ToInt32(response.StatusCode), response.StatusDescription, response.Headers, entityBody);

          #endregion //***** Raw
				}
			}
			catch (WebException wex)
			{
				if (wex.Status == WebExceptionStatus.ProtocolError)
				{
					var response = (HttpWebResponse)wex.Response;

					#region Status Line

					var statusLineElement = responseElement.AppendElement("statusLine");

					//*****
					statusLineElement.AppendElement("protocolName", "HTTP");
					statusLineElement.AppendElement("protocolVersion", response.ProtocolVersion.ToString());
					statusLineElement.AppendElement("statusCode", Convert.ToInt32(response.StatusCode).ToString());
					statusLineElement.AppendElement("statusDescription", response.StatusDescription);

					#endregion //***** Status Line;
				}
				else if (wex.Status == WebExceptionStatus.ServerProtocolViolation)
				{
					//***** NOTE:Exception was thrown when calling https://api.github.com
					//***** Found solution on http://earljon.wordpress.com/2012/08/23/quickfix-server-committed-a-protocol-violation-error-in-net/
					//***** Add <system.net><settings><httpWebRequest useUnsafeHeaderParsing="true"/></settings></system.net> to app.config
					
					//***** TODO:Add wex. Response is not available with this type of exception!
				}
			}
			catch (Exception ex)
			{
				//***** TODO:Non-protocol exception;

				/*var exception = ex;
				UberData dataElement = null;
				while (exception != null)
				{
					var exceptionElement = new UberData { Name = "Message", Value = ex.Message };
					if (dataElement != null) dataElement.Data.Add(exceptionElement);
					exception = exception.InnerException;
					dataElement = exceptionElement;
				}
				if (dataElement == null)
					uber.Error.Add(new UberData { Value = "Unknown exception" });
				else
					uber.Error.Add(dataElement);*/
			}

			return document;
		}

		public void Remove()
		{
			throw new NotImplementedException();
		}

		public void Replace()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

