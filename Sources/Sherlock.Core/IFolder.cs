using System;

namespace Sherlock.Core
{
    public interface IFolder : INode, IComparable<IFolder>
    {
        IFolderCollection Folders
        {
            get;
        }

        IItemCollection Items
        {
            get;
        }
    }
}
