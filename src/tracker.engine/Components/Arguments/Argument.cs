namespace tracker
{
	partial class ArgumentParser
	{
		private class Argument : IArgument
		{
			public IOption Option { get; set; }

			public string Value { get; set; }
		}
	}
}