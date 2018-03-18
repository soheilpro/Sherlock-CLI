using System;
using System.Linq;

namespace Sherlock.Commands
{
    internal class ChangeFolderToRootCommand : CommandBase<object>
    {
        public override string Name
        {
            get
            {
                return "cd/";
            }
        }

        public override string[] Aliases
        {
            get
            {
                return new[] {
                    "/",
                };
            }
        }

        public override string HelpText
        {
            get
            {
                return "Move back to root.";
            }
        }

        public ChangeFolderToRootCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(object options, IContext context)
        {
            var folder = context.Database.RootFolder;

            context.CurrentFolder = folder;
        }
    }
}
