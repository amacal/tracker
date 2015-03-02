namespace tracker
{
	public class Bootstrapper
	{
		private IGeoLocatorFactory CreateGeoLocatorFactory()
		{
			return new GeoLocatorFactory();
		}

		public IArgumentParser CreateArgumentParser()
		{
			return new ArgumentParser();
		}

		public ICommandFactory CreateCommandFactory()
		{
			return 
				new CommandFactory(
					this.CreateGeoLocatorFactory());
		}
	}
}