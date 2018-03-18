using System;
using CommandLine;
using Sherlock.Core;

namespace Sherlock.Commands
{
    internal class RemovePasswordCommand : CommandBase<object>
    {
        public override string Name
        {
            get
            {
                return "rmpwd";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Remove the database password.";
            }
        }

        public RemovePasswordCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(object options, IContext context)
        {
            context.Database.ChangePassword(string.Empty);
        }
    }
}
