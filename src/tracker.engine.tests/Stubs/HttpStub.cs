namespace tracker.tests
{
	public abstract class HttpStub
	{
		public virtual IHttpResponse Issue(IHttpRequest request)
		{
			return new HttpResponse();
		}

		private class HttpResponse : IHttpResponse
		{
			public bool IsSuccessful()
			{
				return true;
			}

			public byte[] GetBody()
			{
				return new byte[0];
			}
		}
	}
}