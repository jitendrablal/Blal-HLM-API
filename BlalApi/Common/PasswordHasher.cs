using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace BlalApi.Common
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32;  // 256 bits
        private const int Iterations = 10000;

        public static string Hash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Iterations))
            {
                var salt = deriveBytes.Salt;
                var key = deriveBytes.GetBytes(KeySize);

                return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
            }
        }

        public static bool Verify(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                var keyToCheck = deriveBytes.GetBytes(KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }
    }
}