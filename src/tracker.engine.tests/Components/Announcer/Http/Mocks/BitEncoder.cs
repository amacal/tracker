using System.Text;

namespace tracker.tests
{
	partial class HttpAnnouncerTests
	{
		private class BitEncoder : BitEncoderStub, IBitEncoder
		{			
			public byte[] Peers = new byte[0];

			public override IBitValue Decode(byte[] data)
			{
				return new BitValue
				{
					Dictionary = new IBitEntry[]
					{
						new BitEntry
						{
							Key = new BitValue { Text = new BitTextString("peers") },
							Value = new BitValue { Text = new BitTextBytes(this.Peers) }
						}
					}
				};
			}
		}

		private class BitValue : IBitValue
		{
			public IBitText Text { get; set; }

			public long Integer { get; set; }

			public IBitValue[] Array { get; set; }

			public IBitEntry[] Dictionary { get; set; }			
		}

		public class BitEntry : IBitEntry
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