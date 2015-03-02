using System;

namespace tracker
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			string commandline = string.Join(" ", args);
			Bootstrapper bootstrapper = new Bootstrapper();

			IArgumentParser parser = bootstrapper.CreateArgumentParser();
			IArgument[] arguments = parser.Parse(commandline);

			ICommandFactory factory = bootstrapper.CreateCommandFactory();
			ICommand command = factory.Create(arguments);

			command.Execute();
		}
	}
}
