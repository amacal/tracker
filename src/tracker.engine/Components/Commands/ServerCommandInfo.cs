namespace tracker
{
	partial class CommandFactory
	{
		private class ServerCommandInfo : ICommandInfo
		{
			private readonly IGeoLocatorFactory geoLocatorFactory;

			public ServerCommandInfo(IGeoLocatorFactory geoLocatorFactory)
			{
				this.geoLocatorFactory = geoLocatorFactory;
			}

			public bool CanHandle(IArgument[] arguments)
			{
				ArgumentReader reader = new ArgumentReader(arguments);

				return reader.ContainsValue("server", 0);
			}

			public ICommand Create(IArgument[] arguments)
			{
				return new ServerCommand(this.geoLocatorFactory);
			}
		}
	}
}