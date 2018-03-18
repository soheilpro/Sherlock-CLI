using System;
using System.IO;
using Sherlock.Core;

namespace Sherlock
{
    internal class ConsolePasswordProvider : IPasswordProvider
    {
        public string GetPassword()
        {
            return ReadLine.ReadPassword("Password? ");
        }
    }
}
