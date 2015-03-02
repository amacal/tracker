using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace tracker.tests
{
	partial class UdpAnnouncerTests
	{
		[Test]
		public void WhenAnnouncingItSendsAnnounceRequest()
		{
			IAnnouncer announcer = this.CreateUdpAnnouncer();
			Announcement announcement = new Announcement();

			announcer.Announce(announcement);

			byte[] data = this.udp.Requests[1].ToBytes();
			byte[] template = new byte[] { 0x00, 0x00, 0x00, 0x01 };

			Assert.That(data.Skip(8).Take(4), Is.EqualTo(template));
		}

		[Test]
		[TestCaseSource(typeof(IssuingAnnounceRequestCaseSource), "All")]
		public void WhenAnnouncingItSendsConnectionRequest(IIssuingAnnounceRequestCase testCase)
		{
			IAnnouncer announcer = this.CreateUdpAnnouncer();

			announcer.Announce(testCase.Announcement);

			byte[] data = this.udp.Requests[1].ToBytes();
			byte[] chunk = data.Skip(testCase.Offset).Take(testCase.Template.Length).ToArray();

			Assert.That(chunk, Is.EqualTo(testCase.Template));
		}

		public interface IIssuingAnnounceRequestCase
		{
			int Offset { get;}
			byte[] Template { get; }
			IAnnouncement Announcement { get; }
		}

		private static class IssuingAnnounceRequestCaseSource
		{
			public static IEnumerable<IIssuingAnnounceRequestCase> All()
			{
				yield return new IssuingAnnounceRequestCase
				{
					Name = "connection-id",
					Offset = 0,
					Announcement = new Announcement(),
					Template = new byte[8] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "action-id",
					Offset = 8,
					Announcement = new Announcement(),
					Template = new byte[4] { 0x00, 0x00, 0x00, 0x01 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "info-hash",
					Offset = 16,
					Announcement = new Announcement(),
					Template = new byte[20]
					{
						0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10,
						0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x20
					}
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "peer-id",
					Offset = 36,
					Announcement = new Announcement(),
					Template = Encoding.ASCII.GetBytes("TIX0199-c4b7b6f3b4j5")
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "downloaded",
					Offset = 56,
					Announcement = new Announcement(),
					Template = new byte[8] { 0x11, 0x22, 0x10, 0xf4, 0x7d, 0xe9, 0x81, 0x15 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "left",
					Offset = 64,
					Announcement = new Announcement(),
					Template = new byte[8] { 0x20, 0x8d, 0x86, 0x88, 0x61, 0x37, 0x0a, 0xd2 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "uploaded",
					Offset = 72,
					Announcement = new Announcement(),
					Template = new byte[8] { 0x2f, 0xf8, 0xfb, 0x4a, 0xb8, 0x56, 0x6c, 0x35 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "event",
					Offset = 80,
					Announcement = new Announcement(),
					Template = new byte[4] { 0x00, 0x00, 0x00, 0x02 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "without-ip-address",
					Offset = 84,
					Announcement = new AnnouncementWithoutIpAddress(),
					Template = new byte[4] { 0x00, 0x00, 0x00, 0x00 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "custom-ip-address",
					Offset = 84,
					Announcement = new AnnouncementWithIpAddress(),
					Template = new byte[4] { 56, 17, 211, 21 }
				};

				yield return new IssuingAnnounceRequestCase
				{
					Name = "port",
					Offset = 96,
					Announcement = new AnnouncementWithIpAddress(),
					Template = new byte[2] { 0x1f, 0x90 }
				};
			}

			private class IssuingAnnounceRequestCase : IIssuingAnnounceRequestCase
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

				public override byte[] ToBytes()
				{
					return new byte[] { 56, 17, 211, 21, 80, 80 };
				}

				public override string ToString()
				{
					return "56.17.211.21:8080";
				}

				public override int Port
				{
					get { return 8080; }
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
	}
}