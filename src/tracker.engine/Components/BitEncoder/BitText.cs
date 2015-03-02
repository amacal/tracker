using System.Text;

namespace tracker
{
	partial class BitEncoder
	{
		private class BitText : IBitText
		{
			private readonly byte[] data;
			private readonly int offset;
			private readonly int length;

			public BitText(byte[] data, int offset, int length)
			{
				this.data = data;
				this.offset = offset;
				this.length = length;
			}

			public string GetString()
			{
				return Encoding.ASCII.GetString(this.GetBytes());
			}

			public byte[] GetBytes()
			{
				byte[] data = new byte[this.length];

				for (int i = 0; i < this.length; i++)
				{
					data[i] = this.data[this.offset+i];
				}

				return data;
			}

			public int Length
			{
				get { return this.length; }
			}
		}
	}
}