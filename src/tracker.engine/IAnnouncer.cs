namespace tracker
{
	public interface IAnnouncer
	{
		string Name { get; }

		IEndpoint[] Announce(IAnnouncement announcement);
	}

	public interface IAnnouncement
	{
		byte[] Hash { get; }

		string PeerId { get; }

		IEndpoint Endpoint { get; }

		long Uploaded { get; }

		long Downloaded { get; }

		long Left { get; }

		string Event { get; }
	}
}