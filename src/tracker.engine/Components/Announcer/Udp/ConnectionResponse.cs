namespace tracker
{
	partial class UdpAnnouncer
	{
		private class ConnectionResponse
		{
			private readonly IUdpResponse data;

			public ConnectionResponse(IUdpResponse data)
			{
				this.data = data;
			}

			public IConnectionId Connection
			{
				get { return new ConnectionId(this.data); }
			}

			private class ConnectionId : IConnectionId
			{
				private readonly IUdpResponse data;

				public ConnectionId(IUdpResponse data)
				{
					this.data = data;
				}

				public byte[] ToBytes()
				{
					byte[] binary = this.data.ToBytes();
					byte[] result = new byte[8];

					for (int i = 0; i < 8; i++)
					{
						result[i] = binary[i+8];
					}

					return result;
				}
			}
		}
	}
}