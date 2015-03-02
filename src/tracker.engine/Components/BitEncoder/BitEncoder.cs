using System;
using System.Collections.Generic;
using System.IO;

namespace tracker
{
	public partial class BitEncoder : IBitEncoder
	{
		public byte[] Encode(IBitValue data)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				this.Encode(stream, data);
				return stream.ToArray();
			}
		}

		private void Encode(MemoryStream stream, IBitValue data)
		{
			if (data.Dictionary != null)
			{
				this.Write(stream, "d");

				foreach (IBitEntry entry in data.Dictionary)
				{
					this.Encode(stream, entry.Key);
					this.Encode(stream, entry.Value);
				}

				this.Write(stream, "e");
			}

			else if (data.Array != null)
			{
				this.Write(stream, "l");

				foreach (IBitValue item in data.Array)
				{
					this.Encode(stream, item);
				}

				this.Write(stream, "e");
			}

			else if (data.Text != null)
			{
				this.Write(stream, data.Text.Length.ToString());
				this.Write(stream, ":");
				this.Write(stream, data.Text.GetBytes());
			}

			else
			{
				this.Write(stream, "i");
				this.Write(stream, data.Integer.ToString());
				this.Write(stream, "e");
			}
		}

		private void Write(Stream stream, String data)
		{
			byte[] bytes = new byte[data.Length];

			for (int i = 0; i < bytes.Length; i++)
			{
				bytes[i] = (byte)data[i];
			}

			stream.Write(bytes, 0, bytes.Length);
		}

		private void Write(Stream stream, byte[] data)
		{
			stream.Write(data, 0, data.Length);
		}

		public IBitValue Decode(byte[] data)
		{
			int position = 0;
			return this.Decode(data, ref position);
		}

		private IBitValue Decode(byte[] data, ref int position)
		{
			switch (data[position])
			{
				case 0x69:
					position++;
					return this.DecodeInteger(data, ref position);

				case 0x64:
					position++;
					return this.DecodeDictionary(data, ref position);

				case 0x6c:
					position++;
					return this.DecodeArray(data, ref position);

				case 0x30:
				case 0x31:
				case 0x32:
				case 0x33:
				case 0x34:
				case 0x35:
				case 0x36:
				case 0x37:
				case 0x38:
				case 0x39:
					return this.DecodeString(data, ref position);
			}

			return new BitValue();
		}

		private IBitValue DecodeString(byte[] data, ref int position)
		{
			int offset = 0;
			int length = 0;

			while (data[position] != 0x3a)
			{
				length = length * 10 + (data[position++] - 0x30);
			}

			offset = position + 1;
			position = position + length + 1;

			return new BitValue { Text = new BitText(data, offset, length) };
		}

		private IBitValue DecodeInteger(byte[] data, ref int position)
		{
			long number = 0L;

			while (data[position] != 0x65)
			{
				number = number * 10 + (data[position++] - 0x30);
			}

			position++;
			return new BitValue { Integer = number };
		}

		private IBitValue DecodeArray(byte[] data, ref int position)
		{
			List<IBitValue> array = new List<IBitValue>();

			while (data[position] != 0x65)
			{
				array.Add(this.Decode(data, ref position));
			}

			return new BitValue { Array = array.ToArray() };
		}

		private IBitValue DecodeDictionary(byte[] data, ref int position)
		{
			List<IBitEntry> entries = new List<IBitEntry>();

			while (data[position] != 0x65)
			{			
				IBitValue key = this.Decode(data, ref position);
				IBitValue value = this.Decode(data, ref position);

				entries.Add(new BitEntry { Key = key, Value = value });
			}

			return new BitValue { Dictionary = entries.ToArray() };
		}
	}
}