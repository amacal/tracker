using NUnit.Framework;

namespace tracker.tests
{
	partial class HttpAnnouncerTests
	{
		[Test]
		public void WhenAnnouncingItIssuesTheRequest()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			Assert.That(this.http.Request, Is.Not.Null);
		}

		[Test]
		public void WhenAnnouncingItIssuesHostName()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringStarting("http://example.com/"));
		}

		public void WhenAnnouncingItIssuesPortNumber()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringStarting("http://example.com/"));			
		}

		[Test]
		public void WhenAnnouncingItIssuesHash()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("info_hash="));
		}

		[Test]
		public void WhenAnnouncingItIssuesPeerId()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("peer_id="));
		}

		[Test]
		public void WhenAnnouncingItIssuesPort()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new AnnouncementWithPortNumber();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("port=8080"));
		}

		[Test]
		public void WhenAnnouncingItIssuesUploaded()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("uploaded=123456"));
		}

		[Test]
		public void WhenAnnouncingItIssuesDownloaded()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("downloaded=76543210"));
		}

		[Test]
		public void WhenAnnouncingItIssuesLeft()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("left=1024"));
		}

		[Test]
		public void WhenAnnouncingItIssuesEvent()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("event=started"));
		}

		[Test]
		public void WhenAnnouncingItIssuesCompact()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("compact=1"));
		}

		[Test]
		public void WhenAnnouncingWithoutIpAddressItDoesntIssueIt()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new AnnouncementWithoutIpAddress();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.Not.StringContaining("ip="));
		}

		[Test]
		public void WhenAnnouncingWithIpAddressItIssuesIt()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new AnnouncementWithIpAddress();

			announcer.Announce(announcement);

			string url = this.http.Request.GetUrl();
			Assert.That(url, Is.StringContaining("ip=56.17.211.21"));			
		}

		private class AnnouncementWithIpAddress : Announcement
		{
			public override IEndpoint Endpoint
			{
				get { return new EndpointImpl(); }
			}

			private class EndpointImpl : EndpointStub, IEndpoint
			{
				public override IIpAddress Address
				{
					get { return new IpAddressImpl(); }
				}
			}

			private class IpAddressImpl : IpAddressStub, IIpAddress
			{
				public override ulong ToInteger()
				{
					return 940692245L;
				}

				public override string ToString()
				{
					return "56.17.211.21";
				}
			}
		}

		private class AnnouncementWithoutIpAddress : Announcement
		{
			public override IEndpoint Endpoint
			{
				get { return new EndpointImpl(); }
			}			

			private class EndpointImpl : EndpointStub, IEndpoint
			{
				public override IIpAddress Address
				{
					get { return null; }
				}
			}
		}

		private class AnnouncementWithPortNumber : Announcement
		{
			public override IEndpoint Endpoint
			{
				get { return new EndpointImpl(); }
			}			

			private class EndpointImpl : EndpointStub, IEndpoint
			{
				public override int Port
				{
					get { return 8080; }
				}
			}
		}
	}
}