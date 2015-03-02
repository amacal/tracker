namespace tracker
{
	partial class CommandFactory
	{
		private class FallbackCommandInfo : ICommandInfo
		{
			public bool CanHandle(IArgument[] arguments)
			{
				return true;
			}

			public ICommand Create(IArgument[] arguments)
			{
				return new FallbackCommand();
			}
		}
	}
}