namespace tracker.tests
{
	public abstract class AnnouncementStub
	{
		public virtual byte[] Hash
		{
			get { return new byte[20]; }
		}

		public virtual string PeerId
		{
			get { return new string('A', 20); }
		}

		public virtual IEndpoint Endpoint
		{
			get { return new EndpointImpl(); }
		}

		public virtual long Uploaded
		{
			get { return 0L; }
		}

		public virtual long Downloaded
		{
			get { return 0L; }
		}

		public virtual long Left
		{
			get { return 1024L; }
		}

		public virtual string Event
		{
			get { return "started"; }
		}

		private class EndpointImpl : EndpointStub, IEndpoint
		{
		}
	}
}