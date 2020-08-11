using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace GeoService.BLL.Utils
{
    public class PasswordHash
    {
        private const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
        private const int Pbkdf2IterCount = 1000;
        private const int Pbkdf2SubkeyLength = 256 / 8;
        private const int SaltSize = 128 / 8;
        private readonly byte[] salt;

        public string Salt => Convert.ToBase64String(salt);

        public PasswordHash()
        {
            salt = new byte[SaltSize];
            RandomNumberGenerator.Create().GetBytes(salt);
        }

        public PasswordHash(string strSalt)
        {
            salt = Convert.FromBase64String(strSalt);
        }

        public string Hash(string password)
        {
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public bool Verify(string hashedPassword, string providedPassword)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            if (decodedHashedPassword.Length == 0
                || decodedHashedPassword.Length != (1 + SaltSize + Pbkdf2SubkeyLength))
                return false;

            Buffer.BlockCopy(decodedHashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(decodedHashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);
            byte[] actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
            
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
    }
}


