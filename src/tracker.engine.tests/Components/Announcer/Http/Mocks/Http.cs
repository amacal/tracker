using System;

namespace tracker.tests
{
	partial class HttpAnnouncerTests
	{
		private class Http : HttpStub, IHttp
		{
			public IHttpRequest Request;
			public IHttpResponse Response;

			public override IHttpResponse Issue(IHttpRequest request)
			{
				this.Request = request;

				return this.Response ?? base.Issue(request);
			}
		}
	}
}