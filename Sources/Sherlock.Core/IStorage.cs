using System;
using System.IO;

namespace Sherlock.Core
{
    public interface IStorage
    {
        bool Exists(string path);

        Stream Load(string path);

        void Save(string path, Stream data);
    }
}
