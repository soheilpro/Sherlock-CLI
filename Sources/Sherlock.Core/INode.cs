using System;

namespace Sherlock.Core
{
    public interface INode
    {
        IFolder Parent
        {
            get;
        }

        string Name
        {
            get;
        }
    }
}
