using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class DeleteFolderCommand : CommandBase<DeleteFolderOptions>
    {
        public override string Name
        {
            get
            {
                return "rmdir";
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
                return "Delete folder.";
            }
        }

        public DeleteFolderCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetFolderSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(DeleteFolderOptions options, IContext context)
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

            context.Database.DeleteFolder(folder);
            context.CurrentFolder = folder.Parent;
        }
    }

    internal class DeleteFolderOptions
    {
        [Value(0, Required = true)]
        public string Folder
        {
            get;
            set;
        }
    }
}
