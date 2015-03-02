using System.Net;
using System.Net.Sockets;

namespace tracker
{
	partial class Udp
	{
		private class UdpSession : IUdpSession
		{
			private readonly UdpClient client;
			private bool disposed;

			public UdpSession(UdpClient client)
			{
				this.client = client;
			}

			public void Send(IUdpRequest request)
			{
				this.client.Send(request.ToBytes(), request.Length);
			}

			public IUdpResponse Receive()
			{
				IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
				return new Response(client.Receive(ref endpoint));
			}

			public void Dispose()
			{
				if (this.disposed == false)
				{
					this.disposed = true;
					this.client.Dispose();
				}
			}
		}
	}
}