using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class ShowItemCommand : CommandBase<ShowItemOptions>
    {
        public override string Name
        {
            get
            {
                return "show";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new[] {
                    "cat",
                };
            }
        }

        public override string Arguments
        {
            get
            {
                return "<item>";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Show item's value.";
            }
        }

        public ShowItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(ShowItemOptions options, IContext context)
        {
            var items = context.CurrentFolder.FindItems(options.Spec).ToArray();

            if (items.Length == 0)
            {
                ConsoleHelper.PrintError("Item not found.");
                return;
            }

            if (items.Length > 1)
            {
                ConsoleHelper.PrintError("Too many matches:");
                ConsoleHelper.PrintItems(items);
                return;
            }

            var item = items.Single();

            using (new ColoredConsole(ConsoleColor.DarkGreen))
                Console.WriteLine(item.Value);
        }
    }

    internal class ShowItemOptions
    {
        [Value(0, Required = true)]
        public string Spec
        {
            get;
            set;
        }
    }
}
