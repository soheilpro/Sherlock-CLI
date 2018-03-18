using System;
using System.Collections.Generic;
using Sherlock.Core;

namespace Sherlock
{
    internal interface IController
    {
        ICollection<ICommand> GetCommands();

        void Run();
    }
}
