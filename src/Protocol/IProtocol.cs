	public interface IProtocol
	{
		void Append();
		void Partial();
		string Read(Uri uri, string userName = "", string password = "");
		void Remove();
		void Replace();
	}
