using System;

namespace Sherlock
{
    internal interface IPasswordProvider
    {
        bool CanProvideNewPassword();

        string GetPassword();
    }
}
