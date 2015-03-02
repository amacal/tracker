using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace tracker.tests
{
	partial class UdpAnnouncerTests
	{
		[Test]
		public void WhenAnnouncingItSendsConnectionRequestOfLength16()
		{
			IAnnouncer announcer = this.CreateUdpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			IUdpRequest request = this.udp.Requests[0];
			Assert.That(request, Has.Length.EqualTo(16));
		}

		[Test]
		[TestCaseSource(typeof(IssuingConnectionRequestCaseSource), "All")]
		public void WhenAnnouncingItSendsConnectionRequest(IIssuingConnectionRequestCase testCase)
		{
			IAnnouncer announcer = this.CreateUdpAnnouncer();

			announcer.Announce(testCase.Announcement);

			byte[] data = this.udp.Requests[0].ToBytes();
			byte[] chunk = data.Skip(testCase.Offset).Take(testCase.Template.Length).ToArray();

			Assert.That(chunk, Is.EqualTo(testCase.Template));
		}

		public interface IIssuingConnectionRequestCase
		{
			int Offset { get;}
			byte[] Template { get; }
			IAnnouncement Announcement { get; }
		}

		private static class IssuingConnectionRequestCaseSource
		{
			public static IEnumerable<IIssuingConnectionRequestCase> All()
			{
				yield return new IssuingConnectionRequestCase
				{
					Name = "connection-id",
					Offset = 0,
					Announcement = new Announcement(),
					Template = new byte[] { 0x00, 0x00, 0x04, 0x17, 0x27, 0x10, 0x19, 0x80 }
				};

				yield return new IssuingConnectionRequestCase
				{
					Name = "action-id",
					Offset = 8,
					Announcement = new Announcement(),
					Template = new byte[] { 0x00, 0x00, 0x00, 0x00 }
				};
			}

			private class IssuingConnectionRequestCase : IIssuingConnectionRequestCase
			{
				public string Name { get; set; }
				public int Offset { get; set; }
				public byte[] Template { get; set; }
				public IAnnouncement Announcement { get; set; }

				public override string ToString()
				{
					return this.Name;
				}
			}
		}
	}
}