namespace tracker
{
	partial class UdpAnnouncer
	{
		private class AnnouncementResponse
		{
			private readonly IUdpResponse data;

			public AnnouncementResponse(IUdpResponse data)
			{
				this.data = data;
			}

			public IEndpoint[] GetPeers()
			{
				int length = (this.data.Length - 20) / 6;
				IEndpoint[] endpoints = new IEndpoint[length];
				byte[] binary = this.data.ToBytes();

				for (int i = 20, j = 0; j < length; i+=6, j++)
				{
					endpoints[j] = new EndPoint(binary, i);
				}

				return endpoints;
			}

			private class EndPoint : IEndpoint
			{
				private readonly byte[] data;
				private readonly int offset;

				public EndPoint(byte[] data, int offset)
				{
					this.data = data;
					this.offset = offset;
				}

				public IIpAddress Address
				{
					get { return new IpAddress(this.data, this.offset); }
				}

				public int Port
				{
					get { return (((int)this.data[this.offset+4]) << 8) + this.data[this.offset+5]; }
				}

				public byte[] ToBytes()
				{
					byte[] output = new byte[6];

					for (int i = 0; i < 6; i++)
					{
						output[i] = this.data[this.offset+i];
					}

					return output;
				}

				public override string ToString()
				{
					return string.Format("{0}.{1}.{2}.{3}:{4}", (byte)this.data[this.offset], (byte)this.data[this.offset+1], (byte)this.data[this.offset+2], (byte)this.data[this.offset+3], this.Port);
				}
			}

			private class IpAddress : IIpAddress
			{
				private readonly byte[] data;
				private readonly int offset;

				public IpAddress(byte[] data, int offset)
				{
					this.data = data;
					this.offset = offset;
				}

				public ulong ToInteger()
				{
					return ((ulong)this.data[this.offset] << 24) + ((ulong)this.data[this.offset+1] << 16) + ((ulong)this.data[this.offset+2] << 8) + (ulong)this.data[this.offset+3];
				}

				public byte[] ToBytes()
				{
					byte[] output = new byte[4];

					for (int i = 0; i < 4; i++)
					{
						output[i] = this.data[this.offset+i];
					}

					return output;
				}
			}
		}
	}
}