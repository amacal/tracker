namespace tracker
{
	public interface IHttpTracker
	{
		string HostName { get; }

		int Port { get; }

		string Resource { get; }
	}
}