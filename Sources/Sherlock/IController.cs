using System;
using System.Collections.Generic;

namespace Sherlock
{
    internal interface IController
    {
        ICollection<ICommand> GetCommands();

        void Run();
    }
}
