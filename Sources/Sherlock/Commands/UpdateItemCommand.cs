using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
  internal class UpdateItemCommand : CommandBase<UpdateItemOptions>
    {
        public override string Name
        {
            get
            {
                return "update";
            }
        }

        public override string Arguments
        {
            get
            {
                return "<item> <value> [--secret] [--no-secret]";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Update item.";
            }
        }

        public UpdateItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (arg.StartsWith("-"))
                return new string[] { "--secret", "--no-secret" };

            if (index == 1)
                return GetItemSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(UpdateItemOptions options, IContext context)
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
            var value = item.Value;
            var isSecret = item.IsSecret;

            if (!string.IsNullOrEmpty(options.Value))
                value = options.Value;

            if (options.Secret)
                isSecret = true;

            if (options.NoSecret)
                isSecret = false;

            context.Database.UpdateItem(item, value, isSecret);
        }
    }

    internal class UpdateItemOptions
    {
        [Value(0, Required = true)]
        public string Item
        {
            get;
            set;
        }

        [Value(1)]
        public string Value
        {
            get;
            set;
        }

        [Option("secret")]
        public bool Secret
        {
            get;
            set;
        }

        [Option("no-secret")]
        public bool NoSecret
        {
            get;
            set;
        }
    }
}
