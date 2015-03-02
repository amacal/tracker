using System.Collections.Generic;

namespace tracker
{
	public interface IGeoLocator
	{
		ICountry GetCountry(IIpAddress address);
	}

	public interface IGeoLocatorFactory
	{
		IGeoLocatorSource OpenSource(string filename);

		IGeoLocator Create(IGeoLocatorSource source);
	}

	public interface IGeoLocatorSource
	{
		IEnumerable<IGeoEntry> GetEntries();
	}

	public interface IGeoEntry
	{
		IIpAddress From { get; }

		IIpAddress To { get; }

		ICountry Country { get; }
	}

	public interface ICountry
	{
		string Code { get; }

		string Name { get; }
	}
}