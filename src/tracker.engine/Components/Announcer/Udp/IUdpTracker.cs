namespace tracker
{
	public interface IUdpTracker
	{
		string HostName { get; }

		int Port { get; }
	}
}