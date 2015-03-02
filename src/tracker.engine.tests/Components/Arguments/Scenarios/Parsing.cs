using NUnit.Framework;

namespace tracker.tests
{
	partial class ArgumentsTests
	{
		[Test]
		public void WhenParsingNothingItReturnsEmptyArray()
		{
			IArgumentParser parser = this.CreateArgumentParser();

			IArgument[] arguments = parser.Parse("");

			Assert.That(arguments, Is.Empty);
		}

		[Test]
		public void WhenParsingNotOptionItReturnsValue()
		{
			IArgumentParser parser = this.CreateArgumentParser();

			IArgument[] arguments = parser.Parse("abc");

			Assert.That(arguments[0].Value, Is.EqualTo("abc"));
		}

		[Test]
		public void WhenParsingShortOptionItReturnsIt()
		{
			IArgumentParser parser = this.CreateArgumentParser();

			IArgument[] arguments = parser.Parse("-a");

			Assert.That(arguments[0].Option.Short, Is.EqualTo('a'));
		}

		[Test]
		public void WhenParsingShortOptionWithValueItReturnsIt()
		{
			IArgumentParser parser = this.CreateArgumentParser();

			IArgument[] arguments = parser.Parse("-a value");

			Assert.That(arguments[0].Option.Short, Is.EqualTo('a'));
			Assert.That(arguments[0].Value, Is.EqualTo("value"));
		}

		[Test]
		public void WhenParsingLongOptionItReturnsIt()
		{
			IArgumentParser parser = this.CreateArgumentParser();

			IArgument[] arguments = parser.Parse("--option");

			Assert.That(arguments[0].Option.Long, Is.EqualTo("option"));
		}

		[Test]
		public void WhenparsingLongOptionWithValueItReturnsIt()
		{
			IArgumentParser parser = this.CreateArgumentParser();

			IArgument[] arguments = parser.Parse("--option value");

			Assert.That(arguments[0].Option.Long, Is.EqualTo("option"));
			Assert.That(arguments[0].Value, Is.EqualTo("value"));
		}
	}
}