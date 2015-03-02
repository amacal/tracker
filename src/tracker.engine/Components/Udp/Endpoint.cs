using System.Net;

namespace tracker
{
	partial class Udp
	{
		private class Endpoint : IEndpoint
		{
			private readonly IPHostEntry entry;
			private readonly int port;

			public Endpoint(IPHostEntry entry, int port)
			{
				this.entry = entry;
				this.port = port;
			}

			public IIpAddress Address
			{
				get { return new IpAddress(this.entry); }
			}

			public int Port
			{
				get { return this.port; }
			}

			public byte[] ToBytes()
			{
				byte[] result = new byte[6];
				byte[] address = this.Address.ToBytes();

				for (int i = 0; i < 4; i++)
				{
					result[i] = address[i];
				}

				result[4] = (byte)(this.port / 256);
				result[5] = (byte)(this.port % 256);

				return result;
			}

			public override string ToString()
			{
				return this.entry.HostName + ":" + this.port;
			}

			private class IpAddress : IIpAddress
			{
				private readonly IPHostEntry entry;

				public IpAddress(IPHostEntry entry)
				{
					this.entry = entry;
				}

				public ulong ToInteger()
				{
					return 0;
				}

				public byte[] ToBytes()
				{
					return this.entry.AddressList[0].GetAddressBytes();
				}
			}
		}
	}
}