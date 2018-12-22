using System;
using System.IO;
using Sherlock.Core;

namespace Sherlock
{
    internal interface IDatabaseManager
    {
        IDatabase LoadDatabase(string path);

        void SaveDatabase(IDatabase database, string path);
    }
}
