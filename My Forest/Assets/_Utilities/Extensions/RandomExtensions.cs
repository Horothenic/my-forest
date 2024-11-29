using System;
using System.Linq;
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
        
        public static float GenerateFloatFromPhrase(string phrase, float min = 0, float max = 100)
        {
            if (string.IsNullOrEmpty(phrase)) return 0f;
            
            var hash = 0;
            foreach (var c in phrase)
            {
                hash = (hash * 31 + c) % int.MaxValue;
            }
            
            var normalized = Math.Abs(hash) / (float)int.MaxValue;
            return min + normalized * (max - min);
        }
    }
}
