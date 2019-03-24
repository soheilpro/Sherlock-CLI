using System;
using System.IO;
using Sherlock.Core;

namespace Sherlock
{
    internal class DatabaseManager : IDatabaseManager
    {
        private IStorage _storage;
        private IPasswordProvider _passwordProvider;

        public DatabaseManager(IStorage storage, IPasswordProvider passwordProvider)
        {
            _storage = storage;
            _passwordProvider = passwordProvider;
        }

        public IDatabase LoadDatabase(string path)
        {
            using (var stream = _storage.Load(path))
            {
                var database = LoadDatabase(stream, string.Empty);

                if (database != null)
                    return database;

                do
                {
                    var password = _passwordProvider.GetPassword();
                    database = LoadDatabase(stream, password);

                    if (database != null)
                        return database;
                }
                while (_passwordProvider.CanProvideNewPassword());

                return null;
            }
        }

        private IDatabase LoadDatabase(Stream stream, string password)
        {
            stream.Seek(0, SeekOrigin.Begin);

            try
            {
                var database = new Database();
                database.Load(stream, password);

                return database;
            }
            catch (InvalidPasswordException)
            {
                return null;
            }
        }

        public void SaveDatabase(IDatabase database, string path)
        {
            using (var stream = new MemoryStream())
            {
                database.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                _storage.Save(path, stream);
            }
        }
    }
}
