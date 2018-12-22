using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class RenameItemCommand : CommandBase<RenameItemOptions>
    {
        public override string Name
        {
            get
            {
                return "ren";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new[] {
                    "mv",
                };
            }
        }

        public override string Arguments
        {
            get
            {
                return "<item> <name>";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Rename item.";
            }
        }

        public RenameItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(RenameItemOptions options, IContext context)
        {
            var items = context.CurrentFolder.FindItems(options.Item).ToArray();

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

            context.Database.RenameItem(item, options.NewName);
        }
    }

    internal class RenameItemOptions
    {
        [Value(0, Required = true)]
        public string Item
        {
            get;
            set;
        }

        [Value(1, Required = true)]
        public string NewName
        {
            get;
            set;
        }
    }
}
