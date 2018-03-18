using System;
using Sherlock.Core;

namespace Sherlock
{
    internal interface IContext
    {
        IDatabase Database
        {
            get;
        }

        string DatabasePath
        {
            get;
        }

        IDatabaseManager DatabaseManager
        {
            get;
        }

        IFolder CurrentFolder
        {
            get;
            set;
        }

        bool ShouldExit
        {
            get;
            set;
        }
    }
}
