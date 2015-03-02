using NUnit.Framework;

namespace tracker.tests
{
	[TestFixture]
	public partial class ArgumentsTests
	{
		private IArgumentParser CreateArgumentParser()
		{
			return new ArgumentParser();
		}
	}
}