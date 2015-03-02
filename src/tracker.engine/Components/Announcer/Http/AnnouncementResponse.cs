using System.Collections.Generic;

namespace tracker
{
	partial class HttpAnnouncer
	{
		private class AnnouncementResponse
		{
			private readonly IBitValue data;

			public AnnouncementResponse(IBitValue data)
			{
				this.data = data;
			}

			public IEndpoint[] GetEndpoints()
			{
				List<IEndpoint> endpoints = new List<IEndpoint>();

				if (this.data != null && this.data.Dictionary != null)
				{
					foreach (IBitEntry entry in this.data.Dictionary)
					{
						if (entry.Key.Text != null)
						{
							if (string.Equals(entry.Key.Text.GetString(), "peers") == true)
							{
								this.HandlePeers(entry.Value.Text.GetBytes(), endpoints);
							}
						}
					}
				}

				return endpoints.ToArray();
			}

			private void HandlePeers(byte[] peers, ICollection<IEndpoint> output)
			{
				for (int i = 0; i < peers.Length; i += 6)
				{
					output.Add(new Endpoint(peers, i));
				}
			}
		}

		private class Endpoint : IEndpoint
		{
			private readonly byte[] data;
			private readonly int offset;

			public Endpoint(byte[] data, int offset)
			{
				this.data = data;
				this.offset = offset;
			}

			public IIpAddress Address
			{
				get { return new IpAddress(this.data, this.offset); }
			}

			public override string ToString()
			{
				return string.Format("{0}.{1}.{2}.{3}:{4}", this.data[this.offset], this.data[this.offset+1], this.data[this.offset+2], this.data[this.offset+3], this.Port);
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

			public int Port
			{
				get { return (this.data[this.offset+4] << 8) + this.data[this.offset+5]; }
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