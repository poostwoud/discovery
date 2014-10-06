namespace Discovery.Library.Protocol
{
	public interface IProtocolResponse
	{
        string MediaType { get; }
        string Result { get; }
        string Raw { get; }
	}
}
