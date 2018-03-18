using System;
using System.Collections.Generic;
using System.Linq;
using Sherlock.Core;

namespace Sherlock
{
    internal static class ConsoleHelper
    {
        public static void PrintFolders(this IEnumerable<IFolder> folders)
        {
            using (new ColoredConsole(ConsoleColor.DarkCyan))
                foreach (var folder in folders)
                    Console.WriteLine($"{folder.GetIndex() + 1, 2} [{folder.Name}]");
        }

        public static void PrintItems(this IEnumerable<IItem> items)
        {
            using (new ColoredConsole(ConsoleColor.DarkCyan))
                foreach (var item in items)
                    Console.WriteLine($"{item.GetIndex() + 1, 2} [{item.Name}]");
        }

        public static void PrintError(string message)
        {
            using (new ColoredConsole(ConsoleColor.Red))
                Console.WriteLine(message);
        }
    }
}
