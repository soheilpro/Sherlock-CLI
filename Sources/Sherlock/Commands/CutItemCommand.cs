using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class CutItemCommand : CommandBase<CutItemOptions>
    {
        public override string Name
        {
            get
            {
                return "cut";
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
                return "Cut item.";
            }
        }

        public CutItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(CutItemOptions options, IContext context)
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

            context.Clipboard = item;
        }
    }

    internal class CutItemOptions
    {
        [Value(0, Required = true)]
        public string Item
        {
            get;
            set;
        }
    }
}
