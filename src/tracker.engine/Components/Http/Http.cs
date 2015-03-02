using System;
using System.IO;
using System.Net;

namespace tracker
{
	public partial class Http : IHttp
	{
		public IHttpResponse Issue(IHttpRequest request)
		{
			HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(request.GetUrl());
			httpRequest.Timeout = 1500;

			WebResponse httpResponse = httpRequest.GetResponse();
			byte[] buffer = new byte[1024];

			using (Stream stream = httpResponse.GetResponseStream())
			{
				int read = stream.Read(buffer, 0, 1024);
				Array.Resize(ref buffer, read);
			}

			return new Response(buffer);
		}
	}
}