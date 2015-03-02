namespace tracker
{
	partial class UdpAnnouncer
	{
		private class ConnectionRequest : IUdpRequest
		{		
			public byte[] ToBytes()
			{
				return new byte[] 
				{
					0x00, 0x00, 0x04, 0x17, 0x27, 0x10, 0x19, 0x80,
					0x00, 0x00, 0x00, 0x00,
					0x01, 0x02, 0x03, 0x04
				};
			}

			public int Length
			{
				get { return 16; }
			}
		}
	}
}