namespace tracker
{
	partial class Http
	{
		private class Response : IHttpResponse
		{
			private readonly byte[] buffer;

			public Response(byte[] buffer)
			{
				this.buffer = buffer;
			}

			public bool IsSuccessful()
			{
				return true;
			}

			public byte[] GetBody()
			{
				return this.buffer;
			}
		}		
	}
}