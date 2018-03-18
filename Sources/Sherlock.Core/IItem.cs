using System;

namespace Sherlock.Core
{
    public interface IItem : IComparable<IItem>
    {
        IFolder Parent
        {
            get;
        }

        string Name
        {
            get;
        }

        string Value
        {
            get;
        }

        bool IsSecret
        {
            get;
        }
    }
}
