namespace tracker.tests
{
	public abstract class UdpStub
	{
		public virtual IEndpoint Resolve(string hostname, int port)
		{
			return new DefaultEndpoint();
		}

		public virtual IUdpSession CreateSession(IEndpoint endpoint)
		{
			return new DefaultSession();
		}

		private class DefaultEndpoint : EndpointStub, IEndpoint
		{
		}

		private class DefaultSession : UdpSessionStub, IUdpSession
		{
		}
	}
}