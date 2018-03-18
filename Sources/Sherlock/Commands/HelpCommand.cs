using System;
using System.Linq;
using Sherlock.Core;

namespace Sherlock.Commands
{
    internal class HelpCommand : CommandBase<object>
    {
        public override string Name
        {
            get
            {
                return "help";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Show this help screen.";
            }
        }

        public HelpCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(object options, IContext context)
        {
            foreach (var command in Controller.GetCommands().OrderBy(command => command.Name))
            {
                using (new ColoredConsole(ConsoleColor.DarkCyan))
                    Console.Write(command.Name);

                foreach (var alias in command.Aliases)
                {
                    Console.Write("|");

                    using (new ColoredConsole(ConsoleColor.DarkCyan))
                        Console.Write(alias);
                }

                using (new ColoredConsole(ConsoleColor.DarkCyan))
                {
                    if (!string.IsNullOrEmpty(command.Arguments))
                        Console.Write($" {command.Arguments}");

                    Console.WriteLine();
                }

                Console.WriteLine(command.HelpText);
                Console.WriteLine();
            }
        }
    }
}
