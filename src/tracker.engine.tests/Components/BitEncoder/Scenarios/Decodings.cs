using System.Text;
using NUnit.Framework;

namespace tracker.tests
{
	partial class BitEncoderTests
	{
		[Test]
		public void WhenDecodingEmptyDictionaryItHasNoValue()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("de");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary, Is.Empty);
		}

		[Test]
		public void WhenDecodingDictionaryWithStringPropertyItHasTheKeyAndValue()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:id3:abce");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Key.Text.GetString(), Is.EqualTo("id"));
			Assert.That(value.Dictionary[0].Value.Text.GetString(), Is.EqualTo("abc"));
		}

		[Test]
		public void WhenDecodingDictionaryWithIntegerPropertyItHasTheKeyAndValue()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:idi12ee");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Key.Text.GetString(), Is.EqualTo("id"));
			Assert.That(value.Dictionary[0].Value.Integer, Is.EqualTo(12));
		}

		[Test]
		public void WhenDecodingDictionaryWithNestedObjectItRegistersIt()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:iddee");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Key.Text.GetString(), Is.EqualTo("id"));
		}

		[Test]
		public void WhenDecodingDictionaryWithNestedObjectItHasNestedKeyAndValue()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:idd4:name7:trackeree");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Value.Dictionary[0].Key.Text.GetString(), Is.EqualTo("name"));
			Assert.That(value.Dictionary[0].Value.Dictionary[0].Value.Text.GetString(), Is.EqualTo("tracker"));
		}

		[Test]
		public void WhenDecodingDictionaryWithArrayItRegistersIt()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:idlee");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Key.Text.GetString(), Is.EqualTo("id"));
		}

		[Test]
		public void WhenDecodingDictionaryWithArrayItHasIntegerValue()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:idli12eee");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Value.Array[0].Integer, Is.EqualTo(12));
		}

		[Test]
		public void WhenDecodingDictionaryWithArrayItHasStringValue()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:idl3:abcee");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Value.Array[0].Text.GetString(), Is.EqualTo("abc"));
		}

		[Test]
		public void WhenDecodingDictionaryWithArrayItHasDictionaryValue()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = Encoding.ASCII.GetBytes("d2:idld4:namei13eeee");

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Dictionary[0].Value.Array[0].Dictionary[0].Key.Text.GetString(), Is.EqualTo("name"));
			Assert.That(value.Dictionary[0].Value.Array[0].Dictionary[0].Value.Integer, Is.EqualTo(13));
		}

		[Test]
		public void WhenDecodingStringWithHighCharactersItCanPreserveThem()
		{
			IBitEncoder bencoder = this.CreateBitEncoder();
			byte[] data = new byte[] { 0x32, 0x3a, 0xff, 0xaa };

			IBitValue value = bencoder.Decode(data);

			Assert.That(value.Text.GetBytes(), Is.EqualTo(new byte[] { 0xff, 0xaa }));
		}
	}
}