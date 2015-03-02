using NUnit.Framework;

namespace tracker.tests
{
	partial class HttpAnnouncerTests
	{
		[Test]
		public void WhenReceivedNegativeResponseItReturnsEmptyArray()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			this.http.Response = new InvalidRequest();
			IEndpoint[] endpoints = announcer.Announce(announcement);

			Assert.That(endpoints, Is.Empty);
		}

		[Test]
		public void WhenReceivedNoPeersItReturnsEmptyArray()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();

			this.encoder.Peers = new byte[0];
			IEndpoint[] endpoints = announcer.Announce(announcement);

			Assert.That(endpoints, Is.Empty);
		}

		[Test]
		public void WhenReceivedTwoPeersItReturnsRightCount()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();
			byte[] peers = new byte[] 
			{
				0x0a, 0x0a, 0x0a, 0x05, 0x00, 0x80,
				0x0f, 0x3a, 0x65, 0x12, 0x01, 0x80,
			};

			this.encoder.Peers = peers;
			IEndpoint[] endpoints = announcer.Announce(announcement);

			Assert.That(endpoints, Has.Length.EqualTo(2));
		}

		[Test]
		public void WhenReceivedTwoPeersItReturnsThem()
		{
			IAnnouncer announcer = this.CreateHttpAnnouncer();
			Announcement announcement = new Announcement();
			byte[] peers = new byte[] 
			{
				0x0a, 0x0a, 0xaa, 0x05, 0x00, 0x80,
				0x0f, 0x9a, 0x65, 0x12, 0x01, 0x80,
			};

			this.encoder.Peers = peers;
			IEndpoint[] endpoints = announcer.Announce(announcement);

			Assert.That(endpoints[0].ToString(), Is.EqualTo("10.10.170.5:128"));			
		}

		private class InvalidRequest : IHttpResponse
		{
			public bool IsSuccessful()
			{
				return false;
			}

			public byte[] GetBody()
			{
				return new byte[0];	
			}
		}
	}
}