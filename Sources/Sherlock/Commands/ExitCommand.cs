using System;

namespace Sherlock.Commands
{
    internal class ExitCommand : CommandBase<object>
    {
        public override string Name
        {
            get
            {
                return "exit";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Quit.";
            }
        }

        public ExitCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(object options, IContext context)
        {
            if (context.Database.IsDirty)
            {
                while (true)
                {
                    var answer = ReadLine.Read("You have unsaved chanages. What to do? (save/discard/cancel)? ");

                    if (answer == "save" || answer == "s")
                    {
                        context.DatabaseManager.SaveDatabase(context.Database, context.DatabasePath);
                        break;
                    }
                    else if (answer == "discard" || answer == "d")
                    {
                        break;
                    }
                    else if (answer == "cancel" || answer == "c")
                    {
                        return;
                    }
                }
            }

            context.ShouldExit = true;
        }
    }
}
