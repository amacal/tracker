namespace tracker.tests
{
	partial class UdpAnnouncerTests
	{
		private class UdpTracker : UdpTrackerStub, IUdpTracker
		{
			public override string HostName
			{
				get { return "open.demonii.com"; }
			}

			public override int Port
			{
				get { return 1337; }
			}
		}
	}
}