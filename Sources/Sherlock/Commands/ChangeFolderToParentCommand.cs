using System;
using System.Linq;

namespace Sherlock.Commands
{
    internal class ChangeFolderToParentCommand : CommandBase<object>
    {
        public override string Name
        {
            get
            {
                return "cd..";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new[] {
                    "..",
                };
            }
        }

        public override string HelpText
        {
            get
            {
                return "Move back one level up.";
            }
        }

        public ChangeFolderToParentCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(object options, IContext context)
        {
            var folder = context.CurrentFolder.FindFolders("..").Single();

            context.CurrentFolder = folder;
        }
    }
}
