using System;
using System.Collections.Generic;

namespace Sherlock.Core
{
    public interface IItemCollection : IReadOnlyList<IItem>
    {
        int IndexOf(IItem item);
    }
}
