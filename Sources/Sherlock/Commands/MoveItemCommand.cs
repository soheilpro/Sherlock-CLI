using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class MoveItemCommand : CommandBase<MoveItemOptions>
    {
        public override string Name
        {
            get
            {
                return "mv";
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

        public MoveItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(MoveItemOptions options, IContext context)
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

    internal class MoveItemOptions
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
