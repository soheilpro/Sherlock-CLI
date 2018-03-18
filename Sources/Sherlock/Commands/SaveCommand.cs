using System;

namespace Sherlock.Commands
{
    internal class SaveCommand : CommandBase<object>
    {
        public override string Name
        {
            get
            {
                return "save";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Save changes.";
            }
        }

        public SaveCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(object options, IContext context)
        {
            context.DatabaseManager.SaveDatabase(context.Database, context.DatabasePath);
        }
    }
}
