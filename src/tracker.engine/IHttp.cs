using System;

namespace tracker
{
	public interface IHttp
	{
		IHttpResponse Issue(IHttpRequest request);
	}

	public interface IHttpRequest
	{
		string GetUrl();
	}

	public interface IHttpResponse
	{
		bool IsSuccessful();

		byte[] GetBody();
	}
}