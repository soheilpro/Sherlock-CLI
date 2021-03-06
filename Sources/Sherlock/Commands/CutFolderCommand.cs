using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal class CutFolderCommand : CommandBase<CutFolderOptions>
    {
        public override string Name
        {
            get
            {
                return "cutdir";
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
                return "Cut a folder.";
            }
        }

        public CutFolderCommand(IController controller) : base(controller)
        {
        }

        public override string[] GetSuggestions(string arg, int index, IContext context)
        {
            if (index == 1)
                return GetFolderSuggestions(arg, index, context);

            return base.GetSuggestions(arg, index, context);
        }

        protected override void Execute(CutFolderOptions options, IContext context)
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
                ConsoleHelper.PrintError("Cannot cut the root folder.");
                return;
            }

            context.Clipboard = folder;
        }
    }

    internal class CutFolderOptions
    {
        [Value(0, Required = true)]
        public string Folder
        {
            get;
            set;
        }
    }
}
