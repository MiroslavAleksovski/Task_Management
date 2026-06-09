using System.Security.Cryptography;

namespace Infrastructure
{
    public static class Cryptography
    {

        /// <summary>
        /// Hashes a password using PBKDF2.
        /// </summary>
        public static string HashPassword(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 16, 10000, HashAlgorithmName.SHA256))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] hash = deriveBytes.GetBytes(20);

                byte[] hashWithSalt = new byte[36];
                Array.Copy(salt, 0, hashWithSalt, 0, 16);
                Array.Copy(hash, 0, hashWithSalt, 16, 20);

                return Convert.ToBase64String(hashWithSalt);
            }
        }

        /// <summary>
        /// Verifies a password against its hash.
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            byte[] hashWithSalt = Convert.FromBase64String(hash);
            byte[] salt = new byte[16];
            Array.Copy(hashWithSalt, 0, salt, 0, 16);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashOfInput = deriveBytes.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (hashWithSalt[i + 16] != hashOfInput[i])
                        return false;
                }
            }

            return true;
        }

    }
}
