using System;
using System.IO;

namespace Sherlock.Core
{
    public interface IDatabase
    {
        IFolder RootFolder
        {
            get;
        }

        bool IsDirty
        {
            get;
        }

        void AddFolder(IFolder folder, IFolder parentFolder);

        void AddItem(IItem item, IFolder parentFolder);

        void DeleteFolder(IFolder folder);

        void DeleteItem(IItem item);

        void RenameFolder(IFolder folder, string name);

        void RenameItem(IItem item, string name);

        void UpdateItem(IItem item, string value, bool isSecret);

        void ChangePassword(string password);

        void Load(Stream stream, string password);

        void Save(Stream stream);
    }
}
