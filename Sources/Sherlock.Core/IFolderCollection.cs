using System;
using System.Collections.Generic;

namespace Sherlock.Core
{
    public interface IFolderCollection : IReadOnlyList<IFolder>
    {
        int IndexOf(IFolder folder);
    }
}
