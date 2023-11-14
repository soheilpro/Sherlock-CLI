using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Sherlock.Core
{
    public class Database : IDatabase
    {
        private string _password;
        private IFolder _rootFolder;
        private bool _isDirty;

        public IFolder RootFolder
        {
            get
            {
                return _rootFolder;
            }
        }

        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
        }

        public Database()
        {
            _rootFolder = new Folder();
        }

        public void AddFolder(IFolder folder, IFolder parentFolder)
        {
            ((Folder)folder).Parent = parentFolder;
            ((FolderCollection)parentFolder.Folders).Add(folder);
            ((FolderCollection)parentFolder.Folders).Sort();

            _isDirty = true;
        }

        public void AddItem(IItem item, IFolder parentFolder)
        {
            ((Item)item).Parent = parentFolder;
            ((ItemCollection)parentFolder.Items).Add(item);
            ((ItemCollection)parentFolder.Items).Sort();

            _isDirty = true;
        }

        public void DeleteFolder(IFolder folder)
        {
            ((FolderCollection)folder.Parent.Folders).Remove(folder);

            _isDirty = true;
        }

        public void DeleteItem(IItem item)
        {
            ((ItemCollection)item.Parent.Items).Remove(item);

            _isDirty = true;
        }

        public void RenameFolder(IFolder folder, string name)
        {
            ((Folder)folder).Name = name;
            ((FolderCollection)folder.Parent.Folders).Sort();

            _isDirty = true;
        }

        public void RenameItem(IItem item, string name)
        {
            ((Item)item).Name = name;
            ((ItemCollection)item.Parent.Items).Sort();

            _isDirty = true;
        }

        public void MoveFolder(IFolder folder, IFolder newParentFolder)
        {
            this.DeleteFolder(folder);
            this.AddFolder(folder, newParentFolder);
        }

        public void MoveItem(IItem item, IFolder newParentFolder)
        {
            this.DeleteItem(item);
            this.AddItem(item, newParentFolder);
        }

        public void UpdateItem(IItem item, string value, bool isSecret)
        {
            ((Item)item).Value = value;
            ((Item)item).IsSecret = isSecret;
            ((ItemCollection)item.Parent.Items).Sort();

            _isDirty = true;
        }

        public void ChangePassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                try
                {
                    CryptoHelper.CreateTripleDEPS(password);
                }
                catch (CryptographicException)
                {
                    throw new WeakPasswordException();
                }
            }

            _password = password;
            _isDirty = true;
        }

        public void Load(Stream stream, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    var des = CryptoHelper.CreateTripleDEPS(password);
                    stream = new CryptoStream(stream, des.CreateDecryptor(), CryptoStreamMode.Read);
                }

                var document = XDocument.Load(stream);

                _password = password;
                _rootFolder = ReadFolder(document.Root, null);
                _isDirty = false;
            }
            catch (CryptographicException)
            {
                throw new InvalidPasswordException();
            }
            catch (System.Xml.XmlException)
            {
                throw new InvalidPasswordException();
            }
        }

        public void Save(Stream stream)
        {
            var document = new XDocument(SaveFolder(_rootFolder));

            if (!string.IsNullOrEmpty(_password))
            {
                var des = CryptoHelper.CreateTripleDEPS(_password);

                var cryptoStream = new CryptoStream(stream, des.CreateEncryptor(), CryptoStreamMode.Write);
                document.Save(cryptoStream);
                cryptoStream.FlushFinalBlock();
            }
            else
            {
                document.Save(stream);
            }

            _isDirty = false;
        }

        private static Folder ReadFolder(XElement element, IFolder parent)
        {
            var folder = new Folder();
            folder.Parent = parent;
            folder.Name = element.Attribute("name")?.Value;

            foreach (var folderElement in element.Elements("category"))
                ((FolderCollection)folder.Folders).Add(ReadFolder(folderElement, folder));

            foreach (var itemElement in element.Elements("item"))
                ((ItemCollection)folder.Items).Add(ReadItem(itemElement, folder));

            ((FolderCollection)folder.Folders).Sort();
            ((ItemCollection)folder.Items).Sort();

            return folder;
        }

        private static Item ReadItem(XElement element, IFolder parent)
        {
            var item = new Item();
            item.Parent = parent;
            item.Name = element.Attribute("name").Value;
            item.Value = element.Value;
            item.IsSecret = element.Attribute("type").Value == "password";

            return item;
        }

        private XElement SaveFolder(IFolder folder)
        {
            var isRoot = folder.Parent == null;

            return new XElement(isRoot ? "data" : "category",
                isRoot ? null : new XAttribute("name", folder.Name),
                folder.Folders.Select(SaveFolder),
                folder.Items.Select(SaveItem)
            );
        }

        private XElement SaveItem(IItem item)
        {
            return new XElement("item",
                new XAttribute("name", item.Name),
                new XAttribute("type", item.IsSecret ? "password" : "text"),
                item.Value
            );
        }
    }
}
