namespace tracker
{
	public interface IIpAddress
	{
		ulong ToInteger();

		byte[] ToBytes();
	}
}