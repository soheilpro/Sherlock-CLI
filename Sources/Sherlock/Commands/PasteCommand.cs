using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Sherlock.Core;

namespace Sherlock.Commands
{
    internal class PasteCommand : CommandBase<PasteOptions>
    {
        public override string Name
        {
            get
            {
                return "paste";
            }
        }

        public override string HelpText
        {
            get
            {
                return "Paste the cut item or folder.";
            }
        }

        public PasteCommand(IController controller) : base(controller)
        {
        }

        protected override void Execute(PasteOptions options, IContext context)
        {
            var node = context.Clipboard;

            if (node == null)
            {
                ConsoleHelper.PrintError("Clipboard is empty.");
                return;
            }

            if (node is IFolder)
            {
                context.Database.MoveFolder((IFolder)node, context.CurrentFolder);
                context.Clipboard = null;
            }
            else if (node is IItem)
            {
                context.Database.MoveItem((IItem)node, context.CurrentFolder);
                context.Clipboard = null;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }

    internal class PasteOptions
    {
    }
}
