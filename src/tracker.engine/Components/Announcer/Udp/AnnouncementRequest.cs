namespace tracker
{
	partial class UdpAnnouncer
	{
		private class AnnouncementRequest : IUdpRequest
		{
			private readonly IAnnouncement announcement;
			private readonly IConnectionId connectionId;

			public AnnouncementRequest(IAnnouncement announcement, IConnectionId connectionId)
			{
				this.announcement = announcement;
				this.connectionId = connectionId; 
			}

			public byte[] ToBytes()
			{
				byte[] data = new byte[98];

				this.AppendConnectionId(data);
				this.AppendActionType(data);
				this.AppendTransactionId(data);
				this.AppendHash(data);
				this.AppendPeerId(data);
				this.AppendDownloaded(data);
				this.AppendLeft(data);
				this.AppendUploaded(data);
				this.AppendEvent(data);
				this.AppendIpAddress(data);
				this.AppendNumwant(data);
				this.AppendPort(data);

				return data;
			}

			public int Length
			{
				get { return 98; }
			}

			private void AppendConnectionId(byte[] data)
			{
				byte[] connectionId = this.connectionId.ToBytes();

				for (int i = 0; i < 8; i++)
				{
					data[i] = connectionId[i];
				}
			}

			private void AppendActionType(byte[] data)
			{
				data[11] = 0x01;
			}

			private void AppendTransactionId(byte[] data)
			{
			}

			private void AppendHash(byte[] data)
			{
				for (int i = 0; i < 20; i++)
				{
					data[16+i] = this.announcement.Hash[i];
				}
			}

			private void AppendPeerId(byte[] data)
			{
				for (int i = 0; i < 20; i++)
				{
					data[36+i] = (byte)this.announcement.PeerId[i];
				}
			}

			private void AppendDownloaded(byte[] data)
			{
				this.AppendInteger(data, 56, this.announcement.Downloaded);
			}

			private void AppendLeft(byte[] data)
			{
				this.AppendInteger(data, 64, this.announcement.Left);
			}

			private void AppendUploaded(byte[] data)
			{
				this.AppendInteger(data, 72, this.announcement.Uploaded);
			}

			private void AppendInteger(byte[] data, int offset, long value)
			{
				for (int i = 7; i >= 0; i--)
				{
					data[i+offset] = (byte)(value % 256);
					value = value >> 8;
				}
			}

			private void AppendEvent(byte[] data)
			{
				switch (this.announcement.Event)
				{
					case "started":
						data[83] = 0x02;
						break;

					case "completed":
						data[83] = 0x01;
						break;
						
					case "stopped":
						data[83] = 0x03;
						break;
				}
			}

			private void AppendIpAddress(byte[] data)
			{
				if (this.announcement.Endpoint.Address != null)
				{
					byte[] endpoint = this.announcement.Endpoint.ToBytes();

					for (int i = 0; i < 4; i++)
					{
						data[i+84] = endpoint[i];
					}
				}
			}

			private void AppendNumwant(byte[] data)
			{
				data[95] = 0xff;
			}

			private void AppendPort(byte[] data)
			{
				data[96] = (byte)(this.announcement.Endpoint.Port / 256);
				data[97] = (byte)(this.announcement.Endpoint.Port % 256);
			}
		}
	}
}