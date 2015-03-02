using System;

namespace tracker
{
	public interface IBitEncoder
	{
		IBitValue Decode(byte[] data);

		byte[] Encode(IBitValue data);
	}

	public interface IBitValue
	{
		IBitText Text { get; }

		long Integer { get; }

		IBitValue[] Array { get; }

		IBitEntry[] Dictionary { get; }
	}

	public interface IBitText
	{
		string GetString();

		byte[] GetBytes();

		int Length { get; }
	}

	public interface IBitEntry
	{
		IBitValue Key { get; }

		IBitValue Value { get; }
	}
}