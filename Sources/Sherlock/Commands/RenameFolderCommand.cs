using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class RenameFolderCommand : CommandBase<RenameFolderOptions>
    {
        public override string Name
        {
            get
            {
                return "rendir";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new[] {
                    "mvdir",
                };
            }
        }

        public override string Arguments
        {
            get
            {
                return "<folder> <name>";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Rename folder.";
            }
        }

        public RenameFolderCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetFolderSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(RenameFolderOptions options, IContext context)
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

            if (folder.IsRoot())
            {
                ConsoleHelper.PrintError("Cannot remove root folder.");
                return;
            }

            context.Database.RenameFolder(folder, options.NewName);
        }
    }

    internal class RenameFolderOptions
    {
        [Value(0, Required = true)]
        public string Folder
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
