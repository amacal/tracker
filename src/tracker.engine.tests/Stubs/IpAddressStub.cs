namespace tracker.tests
{
	public abstract class IpAddressStub
	{
		public virtual ulong ToInteger()
		{
			return 2130706433L;
		}

		public virtual byte[] ToBytes()
		{
			return new byte[] { 127, 0, 0, 1 };
		}
	}
}