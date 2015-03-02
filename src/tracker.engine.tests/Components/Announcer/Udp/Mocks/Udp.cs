using System.Collections.Generic;

namespace tracker.tests
{
	public class Udp : UdpStub, IUdp
	{
		public IList<IUdpRequest> Requests = new List<IUdpRequest>();
		public IList<IUdpResponse> Responses = new IUdpResponse[]
		{
			new ConnectionResponse(),
			new AnnounceResponse()
		};

		public override IUdpSession CreateSession(IEndpoint endpoint)
		{
			return new UdpSession(this);
		}

		private class ConnectionResponse : IUdpResponse
		{
			public int Length
			{
				get { return 16; }
			}

			public byte[] ToBytes()
			{
				return new byte[16] 
				{
					0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00,
					0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37
				};
			}
		}

		private class AnnounceResponse : IUdpResponse
		{
			public int Length
			{
				get { return 98; }
			}

			public byte[] ToBytes()
			{
				return new byte[98];
			}
		}
	}
}