using NUnit.Framework;

namespace tracker.tests
{
	[TestFixture]
	public partial class BitEncoderTests
	{
		private IBitEncoder CreateBitEncoder()
		{
			return new BitEncoder();
		}
	}
}