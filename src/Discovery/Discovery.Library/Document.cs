using System;

namespace Discovery.Library
{
	public sealed class Document : IDocument
	{
		#region IDocument Members

		public string MediaType { get; internal set; }

		public string Contents { get; internal set; }

		#endregion
	}
}
