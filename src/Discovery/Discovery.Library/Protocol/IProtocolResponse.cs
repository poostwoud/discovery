namespace Discovery.Library.Protocol
{
	public interface IProtocolResponse
	{
		string Protocol { get; }
    string MediaType { get; }
    string Result { get; }
    string Raw { get; }
	}
}
