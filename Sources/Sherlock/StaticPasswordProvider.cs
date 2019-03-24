using System;
using System.IO;
using Sherlock.Core;

namespace Sherlock
{
    internal class StaticPasswordProvider : IPasswordProvider
    {
        private string _password;

        public StaticPasswordProvider(string password)
        {
            _password = password;
        }

        public string GetPassword()
        {
            return _password;
        }

        public bool CanProvideNewPassword()
        {
            return false;
        }
    }
}
