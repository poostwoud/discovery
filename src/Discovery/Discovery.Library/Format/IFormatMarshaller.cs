namespace Discovery.Library.Format
{
	public interface IFormatMarshaller
	{
		string Marshal(string formatType, string document);
	}
}
