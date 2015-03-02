using System.Collections.Generic;
using System.IO;

namespace tracker
{
	partial class GeoLocatorFactory
	{
		private class GeoLocatorSource : IGeoLocatorSource
		{
			private readonly string filename;

			public GeoLocatorSource(string filename)
			{
				this.filename = filename;
			}

			public IEnumerable<IGeoEntry> GetEntries()
			{
				string content = File.ReadAllText(this.filename);

				int position = 0, element = 0;
				ulong rangeFrom = 0, rangeTo = 0;
				string country = null;

				while (position < content.Length)
				{
					switch (content[position++])
					{
						case '\r':
						case '\n':
							element = 0;
							continue;

						case ',':
							element++;						
							continue;

						case '"':
							continue;

						default:
							position--;
							break;
					}

					if (element == 2)
					{
						rangeFrom = rangeFrom * 10 + ((ulong)content[position] - 0x30);
					}

					if (element == 3)
					{
						rangeTo = rangeTo * 10 + ((ulong)content[position] - 0x30);
					}

					if (element == 4 && country == null)
					{
						country = content.Substring(position, 2);
					}

					if (element == 5 && country != null)
					{
						yield return new GeoEntry
						{
							Country = new Country { Code = country },
							From = new IpAddress(rangeFrom),
							To = new IpAddress(rangeTo)
						};

						rangeTo = 0;
						rangeFrom = 0;
						country = null;
					}

					position++;
				}
			}

			private class GeoEntry : IGeoEntry
			{
				public IIpAddress From { get; set; }
				public IIpAddress To { get; set; }
				public ICountry Country { get; set; }
			}

			private class IpAddress : IIpAddress
			{
				private readonly ulong value;

				public IpAddress(ulong value)
				{
					this.value = value;
				}

				public ulong ToInteger()
				{
					return this.value;
				}

				public byte[] ToBytes()
				{
					return new byte[0];
				}
			}

			private class Country : ICountry
			{
				public string Code { get; set; }

				public string Name { get; set; }
			}
		}
	}
}