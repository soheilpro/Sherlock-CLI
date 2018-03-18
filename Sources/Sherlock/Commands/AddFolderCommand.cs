using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Sherlock.Core;

namespace Sherlock.Commands
{
    internal class AddFolderCommand : CommandBase<AddFolderOptions>
    {
        public override string Name
        {
            get
            {
                return "mkdir";
            }
        }

        public override string Arguments
        {
            get
            {
                return "<name>";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Add a new folder.";
            }
        }

        public AddFolderCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(AddFolderOptions options, IContext context)
        {
            var folder = new Folder();
            folder.Name = options.Name;

            context.Database.AddFolder(folder, context.CurrentFolder);
        }
    }

    internal class AddFolderOptions
    {
        [Value(0, Required = true)]
        public string Name
        {
            get;
            set;
        }
    }
}
