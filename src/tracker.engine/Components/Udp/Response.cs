namespace tracker
{
	partial class Udp
	{
		private class Response : IUdpResponse
		{
			private readonly byte[] data;

			public Response(byte[] data)
			{
				this.data = data;
			}

			public int Length
			{
				get { return this.data.Length; }
			}

			public byte[] ToBytes()
			{
				return this.data;
			}
		}
	}
}