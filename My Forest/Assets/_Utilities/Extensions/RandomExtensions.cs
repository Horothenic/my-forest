using System;
using System.Security.Cryptography;
using System.Text;

namespace UnityEngine
{
    public static class RandomExtensions
    {
        public static int GetSHA256Hash(this string input)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hash = BitConverter.ToInt32(hashBytes, 0);
            return hash;
        }
    }
}
