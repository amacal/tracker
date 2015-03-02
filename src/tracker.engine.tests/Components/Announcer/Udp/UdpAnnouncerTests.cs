using NUnit.Framework;

namespace tracker.tests
{
	[TestFixture]
	public partial class UdpAnnouncerTests
	{
		private Udp udp;
		private UdpTracker tracker;

		[SetUp]
		public void SetUp()
		{
			this.udp = new Udp();
			this.tracker = new UdpTracker();
		}

		private IAnnouncer CreateUdpAnnouncer()
		{
			return new UdpAnnouncer(this.udp, this.tracker);
		}
	}
}