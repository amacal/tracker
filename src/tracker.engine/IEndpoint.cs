namespace tracker
{
	public interface IEndpoint
	{
		IIpAddress Address { get; }

		int Port { get; }

		byte[] ToBytes();

		string ToString();
	}
}