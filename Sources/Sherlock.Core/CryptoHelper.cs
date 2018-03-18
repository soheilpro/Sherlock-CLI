using System;
using System.Security.Cryptography;
using System.Text;

namespace Sherlock.Core
{
    internal class CryptoHelper
    {
        public static TripleDES CreateTripleDEPS(string password)
        {
            var des = TripleDES.Create();
            des.Key = Encoding.UTF8.GetBytes(FixedLengthString(password, 24));
            des.IV = Encoding.UTF8.GetBytes(FixedLengthString(password, 8));

            return des;
        }

        private static string FixedLengthString(string text, int length)
        {
            while (text.Length < length)
                text += text;

            if (text.Length > length)
                text = text.Substring(0, length);

            return text;
        }
    }
}
