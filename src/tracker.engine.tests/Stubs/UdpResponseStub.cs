namespace tracker.tests
{
	public abstract class UdpResponseStub
	{
		public virtual int Length
		{
			get { return 0; }
		}

		public byte[] ToBytes()
		{
			return new byte[0];
		}
	}
}