namespace tracker
{
	partial class BitEncoder
	{
		private class BitValue : IBitValue
		{
			public IBitText Text { get; set; }

			public long Integer { get; set; }

			public IBitValue[] Array { get; set; }

			public IBitEntry[] Dictionary { get; set; }			
		}
	}
}