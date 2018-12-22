using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class DeleteItemCommand : CommandBase<DeleteItemOptions>
    {
        public override string Name
        {
            get
            {
                return "rm";
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
                return "Delete item.";
            }
        }

        public DeleteItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(DeleteItemOptions options, IContext context)
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

            context.Database.DeleteItem(item);
        }
    }

    internal class DeleteItemOptions
    {
        [Value(0, Required = true)]
        public string Item
        {
            get;
            set;
        }
    }
}
