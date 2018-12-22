using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class ChangeFolderCommand : CommandBase<ChangeFolderOptions>
    {
        public override string Name
        {
            get
            {
                return "cd";
            }
        }

        public override string Arguments
        {
            get
            {
                return "<folder>";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Change the current folder.";
            }
        }

        public ChangeFolderCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetFolderSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(ChangeFolderOptions options, IContext context)
        {
            var folders = context.CurrentFolder.FindFolders(options.Folder).ToArray();

            if (folders.Length == 0)
            {
                ConsoleHelper.PrintError("Folder not found.");
                return;
            }

            if (folders.Length > 1)
            {
                ConsoleHelper.PrintError("Too many matches:");
                ConsoleHelper.PrintFolders(folders);
                return;
            }

            var folder = folders.Single();

            context.CurrentFolder = folder;
        }
    }

    internal class ChangeFolderOptions
    {
        [Value(0, Required = true)]
        public string Folder
        {
            get;
            set;
        }
    }
}
