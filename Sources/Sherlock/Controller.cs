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

        public void PrintItemValue(string path)
        {
            var paths = new TokenParser().Parse(path, separator: '/');
            var folder = _context.Database.RootFolder;

            for (var i = 0; i < paths.Length - 1; i++)
            {
                var folderName = paths[i];
                var folders = folder.FindFolders(folderName).ToArray();

                if (folders.Length == 0)
                {
                    Console.WriteLine($"Folder not found: {folderName}");
                    Environment.ExitCode = 1;
                    return;
                }

                if (folders.Length > 1)
                {
                    Console.WriteLine($"Too many matches: {folderName}");
                    Environment.ExitCode = 1;
                    return;
                }

                folder = folders.Single();
            }

            var itemName = paths[paths.Length - 1];
            var items = folder.FindItems(itemName).ToArray();

            if (items.Length == 0)
            {
                Console.WriteLine($"Item not found: {itemName}");
                Environment.ExitCode = 1;
                return;
            }

            if (items.Length > 1)
            {
                Console.WriteLine($"Too many matches: {itemName}");
                Environment.ExitCode = 1;
                return;
            }

            var item = items.Single();

            Console.Write(item.Value);
        }

        public void RunScript(string script)
        {
            var statements = new TokenParser().Parse(script, separator: ';');

            foreach (var statement in statements)
                ExecuteStatemenet(statement);
        }

        public void RunInteractive()
        {
            ReadLine.AutoCompletionHandler = this;

            while (!_context.ShouldExit)
            {
                Console.WriteLine();

                var currentFolderPath = _context.CurrentFolder.GetFullPath();
                var statement = ReadLine.Read($"{currentFolderPath}> ").Trim();

                if (statement.Length == 0)
                    continue;

                ReadLine.AddHistory(statement);

                ExecuteStatemenet(statement);
            }
        }

        private void ExecuteStatemenet(string statement)
        {
            string[] args;

            try
            {
                args = new TokenParser().Parse(statement).ToArray();
            }
            catch (TokenParser.UnclosedQuotationMarkException)
            {
                using (new ColoredConsole(ConsoleColor.Red))
                    Console.WriteLine("Unclosed quotation mark.");

                return;
            }

            var command = FindCommand(args[0]);

            if (command == null)
            {
                using (new ColoredConsole(ConsoleColor.Red))
                    Console.WriteLine("Invalid command.");

                return;
            }

            command.Execute(args.Skip(1).ToArray(), _context);
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
            var args = new TokenParser().Parse(line, ignoreUnclosedQuotes: true);

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
