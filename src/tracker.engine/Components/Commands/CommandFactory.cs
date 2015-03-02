namespace tracker
{
	public partial class CommandFactory : ICommandFactory
	{
		private readonly ICommandInfo[] commands;

		public CommandFactory(IGeoLocatorFactory geoLocatorFactory)
		{
			this.commands = new ICommandInfo[]
			{
				new ServerCommandInfo(geoLocatorFactory)
			};
		}

		public ICommand Create(IArgument[] arguments)
		{
			for (int i = 0; i < this.commands.Length; i++)
			{
				if (this.commands[i].CanHandle(arguments))
				{
					return this.commands[i].Create(arguments);
				}
			}

			return new FallbackCommand();
		}
	}
}