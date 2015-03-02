namespace tracker.tests
{
	public abstract class UdpTrackerStub
	{
		public virtual string HostName
		{
			get { return "example.com"; }
		}

		public virtual int Port
		{
			get { return 6666; }
		}
	}
}