namespace tracker
{
	public interface ICommandFactory
	{
		ICommand Create(IArgument[] arguments);
	}

	public interface ICommand
	{
		void Execute();
	}
}