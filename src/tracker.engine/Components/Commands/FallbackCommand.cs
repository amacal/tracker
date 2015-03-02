namespace tracker
{
	partial class CommandFactory
	{
		private class FallbackCommand : ICommand
		{
			public void Execute()
			{
			}
		}
	}
}