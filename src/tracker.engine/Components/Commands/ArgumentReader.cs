namespace tracker
{
	partial class CommandFactory
	{
		private class ArgumentReader
		{
			private readonly IArgument[] arguments;

			public ArgumentReader(IArgument[] arguments)
			{
				this.arguments = arguments;
			}

			public bool ContainsValue(string value, int position)
			{
				return arguments.Length > position
				    && arguments[position].Value == value;
			}
		}
	}
}