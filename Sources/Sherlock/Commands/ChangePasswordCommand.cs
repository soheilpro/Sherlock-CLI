using System;
using CommandLine;
using Sherlock.Core;

namespace Sherlock.Commands
{
    internal class ChangePasswordCommand : CommandBase<ChangePasswordOptions>
    {
        public override string Name
        {
            get
            {
                return "chpwd";
            }
        }

        public override string Arguments
        {
            get
            {
                return "<password>";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Change the database password.";
            }
        }

        public ChangePasswordCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(ChangePasswordOptions options, IContext context)
        {
            try
            {
                context.Database.ChangePassword(options.NewPassword);
            }
            catch (WeakPasswordException)
            {
                ConsoleHelper.PrintError("This password is too weak.");
            }
        }
    }

    internal class ChangePasswordOptions
    {
        [Value(0, Required = true)]
        public string NewPassword
        {
            get;
            set;
        }
    }
}
