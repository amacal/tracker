using System.Text;

namespace tracker
{
	partial class HttpAnnouncer
	{
		private class AnnouncementRequest : IHttpRequest
		{
			private readonly IHttpTracker httpTracker;
			private readonly IAnnouncement announcement;

			public AnnouncementRequest(IHttpTracker httpTracker, IAnnouncement announcement)
			{
				this.httpTracker = httpTracker;
				this.announcement = announcement;
			}

			public string GetUrl()
			{
				StringBuilder builder = new StringBuilder();

				this.AppendHostName(builder);
				this.AppendQueryMark(builder);
				this.AppendHash(builder);
				this.AppendConcatenation(builder);
				this.AppendPeerId(builder);
				this.AppendConcatenation(builder);
				this.AppendPort(builder);
				this.AppendConcatenation(builder);
				this.AppendUploaded(builder);
				this.AppendConcatenation(builder);
				this.AppendDownloaded(builder);
				this.AppendConcatenation(builder);
				this.AppendLeft(builder);
				this.AppendConcatenation(builder);
				this.AppendEvent(builder);
				this.AppendConcatenation(builder);
				this.AppendCompact(builder);
				this.AppendConcatenation(builder);
				this.AppendNumwant(builder);

				if (this.announcement.Endpoint.Address != null)
				{
					this.AppendConcatenation(builder);
					this.AppendIpAddress(builder);
				}

				return builder.ToString();
			}

			private void AppendHostName(StringBuilder builder)
			{
				builder.Append("http://");
				builder.Append(this.httpTracker.HostName);

				if (this.httpTracker.Port != 80)
				{
					builder.Append(":");
					builder.Append(this.httpTracker.Port);
				}
				
				builder.Append("/");

				if (this.httpTracker.Resource != null)
				{
					builder.Append(this.httpTracker.Resource);
					builder.Append("/");
				}
			}

			private void AppendHash(StringBuilder builder)
			{			
				builder.Append("info_hash=");

				for (int i = 0; i < this.announcement.Hash.Length; i++)
				{
					builder.Append("%");
					builder.Append(this.FormatHex(this.announcement.Hash[i]));
				}
			}

			private void AppendPeerId(StringBuilder builder)
			{
				builder.Append("peer_id=");

				for (int i = 0; i < this.announcement.PeerId.Length; i++)
				{
					builder.Append("%");
					builder.Append(this.FormatHex(this.announcement.PeerId[i]));
				}
			}

			private void AppendPort(StringBuilder builder)
			{
				builder.Append("port=");
				builder.Append(this.announcement.Endpoint.Port);
			}

			private void AppendUploaded(StringBuilder builder)
			{
				builder.Append("uploaded=");
				builder.Append(this.announcement.Uploaded);
			}

			private void AppendDownloaded(StringBuilder builder)
			{
				builder.Append("downloaded=");
				builder.Append(this.announcement.Downloaded);
			}

			private void AppendLeft(StringBuilder builder)
			{
				builder.Append("left=");
				builder.Append(this.announcement.Left);
			}

			private void AppendEvent(StringBuilder builder)
			{
				builder.Append("event=");
				builder.Append(this.announcement.Event);
			}

			private void AppendCompact(StringBuilder builder)
			{
				builder.Append("compact=1");
			}

			private void AppendNumwant(StringBuilder builder)
			{
				builder.Append("numwant=500");
			}

			private void AppendIpAddress(StringBuilder builder)
			{
				builder.Append("ip=");
				builder.Append(this.announcement.Endpoint.Address.ToString());
			}

			private void AppendQueryMark(StringBuilder builder)
			{
				builder.Append("?");
			}

			private void AppendConcatenation(StringBuilder builder)
			{
				builder.Append("&");
			}

			private string FormatHex(byte value)
			{
				return value.ToString("x2");
			}

			private string FormatHex(char value)
			{
				return ((byte)value).ToString("x2");
			}
		}
	}
}