namespace tracker.tests
{
	public abstract class UdpSessionStub
	{
		public virtual void Send(IUdpRequest request)
		{
		}

		public virtual IUdpResponse Receive()
		{
			return new DefaultUdpResponse();
		}

		public virtual void Dispose()
		{
		}

		private class DefaultUdpResponse : UdpResponseStub, IUdpResponse
		{
		}
	}
}