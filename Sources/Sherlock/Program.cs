using System;
using System.IO;
using CommandLine;
using Sherlock.Core;
using Sherlock.Storage.FileSystem;

namespace Sherlock
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var parser = new CommandLine.Parser(configuration => configuration.HelpWriter = null);

            parser.ParseArguments<Options>(args)
                .WithParsed<Options>(Run)
                .WithNotParsed((errors) => Console.WriteLine($"Usage: sherlock <db>"));
        }

        private static void Run(Options options)
        {
            var storage = new FileSystemStorage();
            var passwordProvider = new ConsolePasswordProvider();
            var databaseManager = new DatabaseManager(storage, passwordProvider);

            var databasePath = options.DatabasePath;
            var database = storage.Exists(databasePath) ? databaseManager.LoadDatabase(databasePath) : new Database();

            var context = new Context();
            context.Database = database;
            context.DatabasePath = databasePath;
            context.DatabaseManager = databaseManager;
            context.CurrentFolder = database.RootFolder;

            var controller = new Controller(context);

            controller.Run();
        }

        private class Options
        {
            [Value(0, MetaName = "DatabasePath", Required = true, HelpText = "Path to the database file.")]
            public string DatabasePath
            {
                get;
                set;
            }
        }
    }
}
