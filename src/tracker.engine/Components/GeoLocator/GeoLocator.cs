using System;
using System.Collections.Generic;

namespace tracker
{
	partial class GeoLocatorFactory
	{
		private class GeoLocator : IGeoLocator
		{
			private readonly IGeoEntry[] items;

			public GeoLocator(IGeoEntry[] items)
			{
				this.items = items;
			}

			public ICountry GetCountry(IIpAddress address)
			{
				ulong ipAddress = address.ToInteger();

				foreach (IGeoEntry item in this.items)
				{
					if (item.From.ToInteger() <= ipAddress && item.To.ToInteger() >= ipAddress)
					{
						return item.Country;
					}
				}

				return null;
			}
		}
	}
}