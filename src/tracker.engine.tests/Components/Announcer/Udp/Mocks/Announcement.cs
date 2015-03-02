namespace tracker.tests
{
	partial class UdpAnnouncerTests
	{
		private class Announcement : AnnouncementStub, IAnnouncement
		{
			public override byte[] Hash
			{
				get
				{
					return new byte[] 
					{
						0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10,
						0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x20
					};
				}
			}

			public override string PeerId
			{
				get { return "TIX0199-c4b7b6f3b4j5"; }
			}

			public override long Downloaded
			{
				get { return 1234567890123456789L; }
			}

			public override long Left
			{
				get { return 2345678901234567890L;}
			}

			public override long Uploaded
			{
				get { return 3456789012345678901L; }
			}

			public override string Event
			{
				get { return "started"; }
			}
		}
	}
}