using System;
using System.Linq;
using CommandLine;
using Sherlock.Core;

namespace Sherlock.Commands
{
    internal class ListCommand : CommandBase<ListOptions>
    {
        public override string Name
        {
            get
            {
                return "ls";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new[] {
                    "l",
                };
            }
        }

        public override string Arguments
        {
            get
            {
                return "[-l] [-p]";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Show contents of the current folder.";
            }
        }

        public ListCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (arg.StartsWith("-"))
                return new string[] { "-l", "-p" };

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(ListOptions options, IContext context)
        {
            foreach (var folder in context.CurrentFolder.Folders)
                using (new ColoredConsole(ConsoleColor.DarkCyan))
                    Console.WriteLine($"{folder.GetIndex() + 1, 2} [{folder.Name}]");

            foreach (var item in context.CurrentFolder.Items)
            {
                Console.Write($"{item.GetIndex() + 1, 2} {item.Name}");

                if (options.ShowValues)
                {
                    var value = item.Value;

                    if (item.IsSecret && !options.ShowSecrets)
                        value = new String('*', value.Length);

                    using (new ColoredConsole(ConsoleColor.DarkGreen))
                        Console.Write($" {value}");
                }

                Console.WriteLine();
            }
        }
    }

    internal class ListOptions
    {
        [Option('l')]
        public bool ShowValues
        {
            get;
            set;
        }

        [Option('p')]
        public bool ShowSecrets
        {
            get;
            set;
        }
    }
}
