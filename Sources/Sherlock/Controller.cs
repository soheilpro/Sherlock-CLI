using System;
using System.Collections.Generic;
using System.Linq;
using Sherlock.Commands;
using Sherlock.Core;

namespace Sherlock
{
    internal class Controller : IController, IAutoCompleteHandler
    {
        public IContext _context;
        public ICollection<ICommand> _commands;

        public Controller(IContext context)
        {
            _context = context;
            _commands = new ICommand[] {
                new AddFolderCommand(this),
                new AddItemCommand(this),
                new ChangeFolderCommand(this),
                new ChangeFolderToParentCommand(this),
                new ChangeFolderToRootCommand(this),
                new ChangePasswordCommand(this),
                new CopyItemCommand(this),
                new CutFolderCommand(this),
                new CutItemCommand(this),
                new DeleteFolderCommand(this),
                new DeleteItemCommand(this),
                new ExitCommand(this),
                new HelpCommand(this),
                new ListCommand(this),
                new OpenItemCommand(this),
                new PasteCommand(this),
                new RemovePasswordCommand(this),
                new RenameFolderCommand(this),
                new RenameItemCommand(this),
                new SaveCommand(this),
                new ShowItemCommand(this),
                new UpdateItemCommand(this),
            };
        }

        public ICollection<ICommand> GetCommands()
        {
            return _commands;
        }

        public void Run()
        {
            ReadLine.AutoCompletionHandler = this;

            var argumentParser = new ArgumentParser();

            while (!_context.ShouldExit)
            {
                Console.WriteLine();

                var currentFolderPath = _context.CurrentFolder.GetFullPath();
                var line = ReadLine.Read($"{currentFolderPath}> ").Trim();

                if (line.Length == 0)
                    continue;

                ReadLine.AddHistory(line);

                string[] args;

                try
                {
                    args = argumentParser.Parse(line).ToArray();
                }
                catch (ArgumentParser.UnclosedQuotationMarkException)
                {
                    using (new ColoredConsole(ConsoleColor.Red))
                        Console.WriteLine("Unclosed quotation mark.");

                    continue;
                }

                var command = FindCommand(args[0]);

                if (command == null)
                {
                    using (new ColoredConsole(ConsoleColor.Red))
                        Console.WriteLine("Invalid command.");

                    continue;
                }

                command.Execute(args.Skip(1).ToArray(), _context);
            }
        }

        private ICommand FindCommand(string name)
        {
            foreach (var command in _commands)
            {
                if (command.Name == name)
                    return command;

                foreach (var alias in command.Aliases)
                    if (alias == name)
                        return command;
            }

            return null;
        }

        char[] IAutoCompleteHandler.Separators {
            get;
            set;
        } = new char[] { ' ' };

        string[] IAutoCompleteHandler.GetSuggestions(string line, int cursorIndex)
        {
            var args = new ArgumentParser().Parse(line, true);

            if (line.EndsWith(' '))
                args = args.Union(new string[] { string.Empty }).ToArray();

            if (args.Length == 0)
                return null;

            if (args.Length == 1)
                return _commands.Where(cmd => cmd.Name.StartsWith(args[0], StringComparison.OrdinalIgnoreCase)).Select(cmd => cmd.Name).ToArray();

            var command = FindCommand(args[0]);

            if (command == null)
                return null;

            var index = args.Length - 1;
            var arg = args[index];
            var suggestions = command.GetSuggestions(arg, index, _context);

            if (suggestions != null)
                suggestions = suggestions.Select(suggestion => suggestion.Contains(" ") ? $"\"{suggestion}\"" : suggestion).ToArray();

            return suggestions;
        }
    }
}
