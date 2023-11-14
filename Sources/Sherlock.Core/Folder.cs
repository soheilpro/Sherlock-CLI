using System;

namespace Sherlock.Core
{
    public class Folder : IFolder
    {
        public IFolder Parent
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IFolderCollection Folders
        {
            get;
            set;
        }

        public IItemCollection Items
        {
            get;
            set;
        }

        public Folder()
        {
            Folders = new FolderCollection();
            Items = new ItemCollection();
        }

        public int CompareTo(IFolder other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
