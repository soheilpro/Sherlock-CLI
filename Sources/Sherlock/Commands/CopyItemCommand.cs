using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class CopyItemCommand : CommandBase<CopyItemOptions>
    {
        public override string Name
        {
            get
            {
                return "clip";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new[] {
                    "pbcopy",
                    "pb",
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
                return "Copy item to clipboard.";
            }
        }

        public CopyItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(CopyItemOptions options, IContext context)
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

            Clipboard.Clipboard.Default.SetText(item.Value);
        }
    }

    internal class CopyItemOptions
    {
        [Value(0, Required = true)]
        public string Item
        {
            get;
            set;
        }
    }
}
