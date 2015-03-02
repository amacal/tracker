using NUnit.Framework;

namespace tracker.tests
{
	[TestFixture]
	public partial class HttpAnnouncerTests
	{
		private Http http;
		private BitEncoder encoder;
		private HttpTracker tracker;

		[SetUp]
		public void SetUp()
		{
			this.http = new Http();
			this.encoder = new BitEncoder();
			this.tracker = new HttpTracker();
		}

		private IAnnouncer CreateHttpAnnouncer()
		{
			return new HttpAnnouncer(this.http, this.encoder, this.tracker);
		}
	}
}