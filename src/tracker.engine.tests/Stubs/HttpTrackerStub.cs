namespace tracker.tests
{
	public abstract class HttpTrackerStub
	{
		public virtual string HostName
		{
			get { return "example.com"; }
		}

		public virtual int Port
		{
			get { return 80; }
		}

		public virtual string Resource
		{
			get { return "announce"; }
		}
	}
}