using System.Linq;

namespace tracker
{
	public partial class GeoLocatorFactory : IGeoLocatorFactory
	{
		public IGeoLocator Create(IGeoLocatorSource source)
		{
			return new GeoLocator(source.GetEntries().ToArray());
		}

		public IGeoLocatorSource OpenSource(string filename)
		{
			return new GeoLocatorSource(filename);
		}
	}
}