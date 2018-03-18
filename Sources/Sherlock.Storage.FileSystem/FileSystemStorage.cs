using System;
using System.IO;
using Sherlock.Core;

namespace Sherlock.Storage.FileSystem
{
    public class FileSystemStorage : IStorage
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public Stream Load(string path)
        {
            return File.OpenRead(path);
        }

        public void Save(string path, Stream data)
        {
            using (var fileStream = File.Create(path))
                data.CopyTo(fileStream);
        }
    }
}
