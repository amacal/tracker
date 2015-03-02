namespace tracker.tests
{
	public class UdpSession : UdpSessionStub, IUdpSession
	{
		private readonly Udp owner;
		private int counter;

		public UdpSession(Udp owner)
		{
			this.owner = owner;
		}

		public override void Send(IUdpRequest request)
		{
			this.owner.Requests.Add(request);
		}

		public override IUdpResponse Receive()
		{
			if (this.counter < this.owner.Responses.Count)
			{
				return this.owner.Responses[this.counter++];
			}

			return base.Receive();
		}
	}
}