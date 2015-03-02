using System.Net;
using System.Net.Sockets;

namespace tracker
{
	public partial class UdpAnnouncer : IAnnouncer
	{
		private readonly IUdp udp;
		private readonly IUdpTracker tracker;		

		public UdpAnnouncer(IUdp udp, IUdpTracker tracker)
		{
			this.udp = udp;
			this.tracker = tracker;
		}

		public string Name
		{
			get { return "udp://" + this.tracker.HostName; }
		}

		public IEndpoint[] Announce(IAnnouncement announcement)
		{
			IEndpoint endpoint = this.udp.Resolve(this.tracker.HostName, this.tracker.Port);

			using (IUdpSession session = this.udp.CreateSession(endpoint))
			{
				session.Send(new ConnectionRequest());
				ConnectionResponse connectionResponse = new ConnectionResponse(session.Receive());

				session.Send(new AnnouncementRequest(announcement, connectionResponse.Connection));
				AnnouncementResponse announcementResponse = new AnnouncementResponse(session.Receive());

				return announcementResponse.GetPeers();
			}
		}
	}
}