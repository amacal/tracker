namespace tracker.tests
{
	public abstract class EndpointStub
	{
		public virtual IIpAddress Address
		{
			get { return new IpAddressImpl(); }
		}

		public virtual int Port
		{
			get { return 8080; }
		}

		public override string ToString()
		{
			return "127.0.0.1:8080";
		}

		public virtual byte[] ToBytes()
		{
			return new byte[] { 127, 0, 0, 1, 80, 80 };
		}

		private class IpAddressImpl : IpAddressStub, IIpAddress
		{
		}
	}
}