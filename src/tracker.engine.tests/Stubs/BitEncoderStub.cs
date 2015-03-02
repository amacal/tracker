namespace tracker.tests
{
	public abstract class BitEncoderStub
	{
		public virtual IBitValue Decode(byte[] data)
		{
			return null;
		}

		public virtual byte[] Encode(IBitValue data)
		{
			return new byte[0];
		}
	}
}