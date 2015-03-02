using System.Net;
using System.Net.Sockets;

namespace tracker
{
	public partial class Udp : IUdp
	{
		public IEndpoint Resolve(string hostname, int port)
		{
			IPHostEntry entry = Dns.GetHostEntry(hostname);
			return new Endpoint(entry, port);
		}

		public IUdpSession CreateSession(IEndpoint endpoint)
		{
			UdpClient client = new UdpClient();

			try
			{
				client.Client.SendTimeout = 5000;
				client.Client.ReceiveTimeout = 5000;
				client.Connect(new IPAddress(endpoint.Address.ToBytes()), endpoint.Port);
			}
			catch
			{
				client.Dispose();
				client = null;

				throw;
			}

			return new UdpSession(client);
		}
	}
}