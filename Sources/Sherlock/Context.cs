using System;
using Sherlock.Core;

namespace Sherlock
{
    internal class Context : IContext
    {
        public IDatabase Database
        {
            get;
            set;
        }
        public string DatabasePath
        {
            get;
            set;
        }

        public IDatabaseManager DatabaseManager
        {
            get;
            set;
        }

        public IFolder CurrentFolder
        {
            get;
            set;
        }

        public bool ShouldExit
        {
            get;
            set;
        }
    }
}
