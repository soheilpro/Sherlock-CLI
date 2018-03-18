using System;
using System.Diagnostics;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class OpenItemCommand : CommandBase<OpenItemOptions>
    {
        public override string Name
        {
            get
            {
                return "open";
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
                return "Open item using the default app.";
            }
        }

        public OpenItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(OpenItemOptions options, IContext context)
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

            Process.Start(new ProcessStartInfo() {
                FileName = item.Value,
                UseShellExecute = true,
            });
        }
    }

    internal class OpenItemOptions
    {
        [Value(0, Required = true)]
        public string Spec
        {
            get;
            set;
        }
    }
}
