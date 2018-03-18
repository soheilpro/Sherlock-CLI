using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Sherlock.Core;

namespace Sherlock.Commands
{
    internal class AddItemCommand : CommandBase<AddItemOptions>
    {
        public override string Name
        {
            get
            {
                return "add";
            }
        }

        public override string Arguments
        {
            get
            {
                return "<name> <value> [--secret]";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Add a new item.";
            }
        }

        public AddItemCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (arg.StartsWith("-"))
                return new string[] { "--secret" };

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(AddItemOptions options, IContext context)
        {
            var item = new Item();
            item.Name = options.Name;
            item.Value = options.Value;
            item.IsSecret = options.Secret;

            context.Database.AddItem(item, context.CurrentFolder);
        }
    }

    internal class AddItemOptions
    {
        [Value(0, Required = true)]
        public string Name
        {
            get;
            set;
        }

        [Value(1, Required = true)]
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
    }
}
