using System;
using System.Collections.Generic;

namespace Sherlock
{
    internal interface IController
    {
        ICollection<ICommand> GetCommands();

        void PrintItemValue(string path);

        void RunScript(string script);

        void RunInteractive();
    }
}
