using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discovery.Library
{
	public interface IDocument
	{
		string MediaType { get; }
		string Contents { get; }
	}
}
