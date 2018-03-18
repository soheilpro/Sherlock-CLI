using System;

namespace Sherlock
{
    internal class ColoredConsole : IDisposable
    {
        private ConsoleColor _originalForegroundColor;

        public ColoredConsole(ConsoleColor foregroudColor)
        {
            _originalForegroundColor = Console.ForegroundColor;

            Console.ForegroundColor = foregroudColor;
        }

        public void Dispose()
        {
            Console.ForegroundColor = _originalForegroundColor;
        }
    }
}
