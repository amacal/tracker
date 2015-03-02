namespace tracker.tests
{
	partial class HttpAnnouncerTests
	{
		private class Announcement : AnnouncementStub, IAnnouncement
		{
			public override string PeerId
			{
				get { return "TIX0199-c4b7b6f3b4j5"; }
			}

			public override long Uploaded
			{
				get { return 123456L; }
			}

			public override long Downloaded
			{
				get { return 76543210L; }
			}
		}
	}
}