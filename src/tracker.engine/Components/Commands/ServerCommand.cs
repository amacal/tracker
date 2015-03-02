using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace tracker
{
	partial class CommandFactory
	{
		private class ServerCommand : ICommand
		{
			private readonly IGeoLocatorFactory geoLocatorFactory;

			public ServerCommand(IGeoLocatorFactory geoLocatorFactory)
			{
				this.geoLocatorFactory = geoLocatorFactory;
			}

			public void Execute()
			{
				IGeoLocatorSource source = this.geoLocatorFactory.OpenSource("GeoIPCountryWhois.csv");
				IGeoLocator locator = this.geoLocatorFactory.Create(source);

				HttpListener listener = new HttpListener();
				listener.Prefixes.Add("http://localhost:7070/");
				listener.Start();

				while (listener.IsListening)
				{
					HttpListenerContext context = listener.GetContext();
					Console.WriteLine(context.Request.Url.OriginalString);

					using (MemoryStream accepted = new MemoryStream())
					{
						IAnnouncement announcement = new Announcement(context.Request.Url.OriginalString);
						IHttpTracker[] httpTrackers = 
						{
							 new HttpTracker("tracker.trackerfix.com", 80, "announce"),
						};

						IUdpTracker[] udpTrackers =
						{
							new UdpTracker("open.demonii.com", 1337),
							new UdpTracker("9.rarbg.me", 2710),
							new UdpTracker("tracker.coppersurfer.tk", 80)
						};

						BitEncoder encoder = new BitEncoder();
						BitValue response = new BitValue();

						foreach (IAnnouncer announcer in GetAnnouncers(encoder, httpTrackers, udpTrackers))
						{
							Console.WriteLine("{0}...", announcer.Name);

							try
							{
								IEndpoint[] endpoints = announcer.Announce(announcement);

								foreach (IEndpoint endpoint in endpoints)
								{
									IIpAddress address = endpoint.Address;
									ICountry country = locator.GetCountry(address);

									if (country.Code == "UA" || country.Code == "RU" || country.Code == "BG" || country.Code == "RO")
									{
										accepted.Write(endpoint.ToBytes(), 0, 6);
										Console.WriteLine("{0}:{1}", country.Code, endpoint.ToString());
									}
								}
							}
							catch (Exception)
							{
								Console.WriteLine("Tracker '{0}' failed.", announcer.Name);
							}
						}

						response.Dictionary = new IBitEntry[]
						{
							new BitEntry
							{
								Key = new BitValue { Text = new BitTextString("interval") },
								Value = new BitValue { Integer = 300 }
							},
							new BitEntry
							{
								Key = new BitValue { Text = new BitTextString("peers") },
								Value = new BitValue { Text = new BitTextBytes(accepted.ToArray()) }
							}
						};

						byte[] output = encoder.Encode(response);

						context.Response.OutputStream.Write(output, 0, output.Length);
						context.Response.OutputStream.Close();
					}
				}
			}

			private static IEnumerable<IAnnouncer> GetAnnouncers(
				IBitEncoder encoder,
				IHttpTracker[] httpTrackers,
				IUdpTracker[] udpTrackers)
			{
				foreach (IHttpTracker tracker in httpTrackers)
				{
					yield return new HttpAnnouncer(new Http(), encoder, tracker);
				}

				foreach (IUdpTracker tracker in udpTrackers)
				{
					yield return new UdpAnnouncer(new Udp(), tracker);
				}
			}

			private class HttpTracker : IHttpTracker
			{
				private readonly string hostname;
				private readonly int port;
				private readonly string resource;

				public HttpTracker(string hostname, int port, string resource)
				{
					this.hostname = hostname;
					this.port = port;
					this.resource = resource;
				}

				public string HostName
				{
					get { return this.hostname; }
				}

				public int Port
				{
					get { return this.port; }
				}

				public string Resource
				{
					get { return this.resource; }
				}
			}

			private class UdpTracker : IUdpTracker
			{
				private readonly string hostname;
				private readonly int port;

				public UdpTracker(string hostname, int port)
				{
					this.hostname = hostname;
					this.port = port;
				}

				public string HostName
				{
					get { return this.hostname; }
				}

				public int Port
				{
					get { return this.port; }
				}
			}

			private class Announcement : IAnnouncement
			{
				private readonly string url;

				public Announcement(string url)
				{
					this.url = url;
				}

				public byte[] Hash
				{
					get { return this.ParseHash(); }
				}

				public string PeerId
				{
					get { return this.Parse("peer_id"); }
				}

				public IEndpoint Endpoint
				{
					get { return new Endpoint(Int32.Parse(this.Parse("port"))); }
				}

				public long Uploaded
				{
					get { return Int32.Parse(this.Parse("uploaded")); }
				}

				public long Downloaded
				{
					get { return Int32.Parse(this.Parse("downloaded")); }
				}

				public long Left
				{
					get { return 0L; }
				}

				public string Event
				{
					get { return this.Parse("event"); }
				}

				private string Parse(string key)
				{
					string value = Regex.Match(this.url, @"(\?|&)" + key + @"=(?<value>[^&]+)&").Groups["value"].Value;
					string decoded = Uri.UnescapeDataString(value);

					return decoded;
				}

				private byte[] ParseHash()
				{
					byte[] hash = new byte[20];
					string value = Regex.Match(this.url, @"(\?|&)info_hash=(?<value>[^&]+)&").Groups["value"].Value;

					for (int j = 0, i = 0; i < value.Length; i++)
					{
						if (value[i] == '%')
						{
							hash[j] = (byte)((this.ParseHex(value[i+1]) << 4) + this.ParseHex(value[i+2]));

							j++;
							i += 2;
						}
						else
						{
							hash[j] = (byte)value[i];
							j++;
						}
					}

					return hash;
				}

				private byte ParseHex(char value)
				{
					if (value >= '0' && value <= '9')
					{
						return (byte)(value - '0');
					}

					if (value >= 'a' && value <= 'f')
					{
						return (byte)(value - 'a' + 10);
					}

					if (value >= 'A' && value <= 'F')
					{
						return (byte)(value - 'A' + 10);
					}

					return 0;
				}
			}

			private class Endpoint : IEndpoint
			{
				private readonly int port;

				public Endpoint(int port)
				{
					this.port = port;
				}

				public IIpAddress Address
				{
					get { return null; }
				}

				public int Port
				{
					get { return this.port; }
				}

				public byte[] ToBytes()
				{
					return new byte[] { 0x00, 0x00, 0x00, 0x00, (byte)(this.port / 256), (byte)(this.port % 256) };
				}
			}

			private class BitValue : IBitValue
			{
				public IBitText Text { get; set; }

				public long Integer { get; set; }

				public IBitValue[] Array { get; set; }

				public IBitEntry[] Dictionary { get; set; }			
			}

			private class BitEntry : IBitEntry
			{
				public IBitValue Key { get; set; }

				public IBitValue Value { get; set; }
			}

			private class BitTextString : IBitText
			{
				private readonly string data;

				public BitTextString(string data)
				{
					this.data = data;
				}

				public string GetString()
				{
					return this.data;
				}

				public byte[] GetBytes()
				{
					return Encoding.ASCII.GetBytes(this.data);
				}

				public int Length
				{
					get { return this.data.Length; }
				}
			}

			private class BitTextBytes : IBitText
			{
				private readonly byte[] data;

				public BitTextBytes(byte[] data)
				{
					this.data = data;
				}

				public string GetString()
				{
					return Encoding.ASCII.GetString(this.data);
				}

				public byte[] GetBytes()
				{
					return this.data;
				}

				public int Length
				{
					get { return this.data.Length; }
				}
			}
		}
	}
}