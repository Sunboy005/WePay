using System.Security.Cryptography;
using System.Text;

namespace wepay.Utils
{
    public class WalletPinHasher
    {
        const int keySize = 64;
        const int iterations = 350000;
       
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        const int SaltSize = 128 / 8;         
        static byte[] salt = new byte[SaltSize];
        
        public WalletPinHasher()
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
        }
        

        public static string HashPassword(string password)
        {
           

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
