using System;
using System.Collections.Generic;
using System.Text;

namespace tracker
{
	public partial class HttpAnnouncer : IAnnouncer
	{
		private readonly IHttp http;
		private readonly IBitEncoder encoder;
		private readonly IHttpTracker tracker;

		public HttpAnnouncer(IHttp http, IBitEncoder encoder, IHttpTracker tracker)
		{
			this.http = http;
			this.encoder = encoder;
			this.tracker = tracker;
		}

		public string Name
		{
			get { return "http://" + this.tracker.HostName; }
		}

		public IEndpoint[] Announce(IAnnouncement announcement)
		{
			IHttpRequest request = new AnnouncementRequest(this.tracker, announcement);		
			IHttpResponse response = this.http.Issue(request);

			return this.HandleResponse(response);
		}

		private IEndpoint[] HandleResponse(IHttpResponse response)
		{
			if (response.IsSuccessful() == false)
			{
				return new IEndpoint[0];
			}

			IBitValue data = this.encoder.Decode(response.GetBody());		
			AnnouncementResponse announcementResponse = new AnnouncementResponse(data);

			return announcementResponse.GetEndpoints();
		}
	}
}