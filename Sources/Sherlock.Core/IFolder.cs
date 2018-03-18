using System;

namespace Sherlock.Core
{
    public interface IFolder : IComparable<IFolder>
    {
        IFolder Parent
        {
            get;
        }

        string Name
        {
            get;
        }

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
