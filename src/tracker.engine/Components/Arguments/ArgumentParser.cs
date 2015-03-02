using System.Collections.Generic;

namespace tracker
{
	public partial class ArgumentParser : IArgumentParser
	{
		public IArgument[] Parse(string data)
		{
			int offset = 0;
			List<IArgument> arguments = new List<IArgument>();

			while (offset < data.Length)
			{
				if (data[offset] == '-')
				{
					offset++;
					this.HandleFirstDash(arguments, data, ref offset);
					continue;
				}

				if (char.IsWhiteSpace(data, offset) == true)
				{
					offset++;
					continue;
				}

				Argument argument = new Argument();
				this.HandleArgumentValue(argument, arguments, data, ref offset);
				arguments.Add(argument);
			}

			return arguments.ToArray();
		}

		private void HandleFirstDash(ICollection<IArgument> arguments, string data, ref int offset)
		{
			while (offset < data.Length)
			{
				switch (data[offset])
				{
					case '-':
						offset++;
						this.HandleSecondDash(arguments, data, ref offset);
						break;

					default:
						this.HandleShortOption(arguments, data, ref offset);
						break;
				}
			}
		}

		private void HandleSecondDash(ICollection<IArgument> arguments, string data, ref int offset)
		{
			while (offset < data.Length)
			{
				switch (data[offset])
				{
					default:
						this.HandleLongOption(arguments, data, ref offset);
						break;
				}
			}
		}

		private void HandleShortOption(ICollection<IArgument> arguments, string data, ref int offset)
		{
			if (char.IsLetter(data, offset) == false)
			{
				offset = data.Length;
				return;
			}

			Argument argument = new Argument
			{
				Option = new Option { Short = data[offset] }
			};

			offset++;
			this.HandleArgumentSeparator(argument, arguments, data, ref offset);
			arguments.Add(argument);
		}

		private void HandleLongOption(ICollection<IArgument> arguments, string data, ref int offset)
		{
			int start = offset, length = 0;

			while (offset < data.Length && char.IsWhiteSpace(data, offset) == false)
			{
				offset++;
				length++;
			}

			if (length == 0)
			{				
				offset = data.Length;
				return;
			}

			Argument argument = new Argument
			{
				Option = new Option { Long = data.Substring(start, length) }
			};

			this.HandleArgumentSeparator(argument, arguments, data, ref offset);
			arguments.Add(argument);
		}

		private void HandleArgumentSeparator(Argument argument, ICollection<IArgument> arguments, string data, ref int offset)
		{
			while (offset < data.Length)
			{
				if (char.IsWhiteSpace(data, offset) == false)
				{
					this.HandleArgumentValue(argument, arguments, data, ref offset);
					break;
				}

				offset++;
			}
		}

		private void HandleArgumentValue(Argument argument, ICollection<IArgument> arguments, string data, ref int offset)
		{
			int start = offset, length = 0;

			if (data[offset] == '-')
			{
				offset++;
				return;
			}

			while (offset < data.Length && char.IsWhiteSpace(data, offset) == false)
			{
				offset++;
				length++;
			}

			if (length == 0)
			{
				offset = data.Length;
				return;
			}

			argument.Value = data.Substring(start, length);
		}
	}
}