using System;
using System.Linq;
using CommandLine;

namespace Sherlock.Commands
{
    internal abstract class CommandBase<TOptions> : ICommand
    {
        public IController Controller
        {
            get;
        }

        public abstract string Name {
            get;
        }

        public virtual string[] Aliases
        {
            get
            {
                return new string[0];
            }
        }

        public virtual string Arguments {
            get
            {
                return null;
            }
        }

        public abstract string HelpText {
            get;
        }

        public CommandBase(IController controller)
        {
            Controller = controller;
        }

        public virtual string[] GetSuggestions(string arg, int index, IContext context)
        {
            return null;
        }

        protected string[] GetFolderSuggestions(string arg, int index, IContext context)
        {
            return context.CurrentFolder.Folders.Where(folder => folder.Name.StartsWith(arg, StringComparison.OrdinalIgnoreCase)).Select(folder => folder.Name).ToArray();
        }

        protected string[] GetItemSuggestions(string arg, int index, IContext context)
        {
            return context.CurrentFolder.Items.Where(item => item.Name.StartsWith(arg, StringComparison.OrdinalIgnoreCase)).Select(item => item.Name).ToArray();
        }

        public void Execute(string[] args, IContext context)
        {
            var parser = new CommandLine.Parser(configuration => configuration.HelpWriter = null);

            parser.ParseArguments<TOptions>(args)
                .WithParsed<TOptions>(options => Execute(options, context))
                .WithNotParsed(errors =>
                {
                    var error = errors.First();

                    if (error is CommandLine.MissingRequiredOptionError)
                        ConsoleHelper.PrintError($"Missing required argument.");
                    else if (error is CommandLine.UnknownOptionError e)
                        ConsoleHelper.PrintError($"Unknown argument: {e.Token}");
                    else
                        ConsoleHelper.PrintError(error.ToString());
                });
        }

        protected abstract void Execute(TOptions options, IContext context);
    }
}
