using DevOpsApi.core.api.Services.POSTempus;
using System.Security.Cryptography;
using System.Text;

namespace DevOpsApi.core.api.Services
{
    public sealed class EncryptDecrypt
    {
        private readonly ILogger<ITempusService> logger;

        public EncryptDecrypt(ILogger<ITempusService> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// Encrypts the given string using SHA256 algorithm
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText, bool handle = false)
        {
            var cipherText = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(plainText))
                    return cipherText;

                string passPhrase = "Pas5pr@se";        // can be any string
                string saltValue = "s@1tValue";        // can be any string            
                string hashAlgorithm = "SHA256";             // can be "MD5"
                int passwordIterations = 2;                  // can be any number
                string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
                int keySize = 256;

                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                cipherText = Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception ex)
            {
                if (handle)
                { }
                //this.logger.Write($"Encrypt('{plainText}')", ex, "Framework");
                //this.logger.LogError($"{clock.GetCurrentInstant().ToDateTimeUtc().ToLocalTime()}: {ex.InnerException.Message}");
                else
                    throw new Exception($"{(ex != null ? ex.Message : "")}", ex);
            }
            return cipherText;
        }

        /// <summary>
        /// Decrypts the given string using SHA256 algorithm and passes the string in plain text
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, bool handle = false)
        {
            var plainText = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    return plainText;

                string passPhrase;
                string saltValue;
                string hashAlgorithm;
                int passwordIterations;
                string initVector;
                int keySize;

                passPhrase = "Pas5pr@se";              // can be any string																																																		   
                saltValue = "s@1tValue";              // can be any string
                hashAlgorithm = "SHA256";              // can be "MD5"
                passwordIterations = 2;              // can be any number
                initVector = "@1B2c3D4e5F6g7H8";        // must be 16 bytes
                keySize = 256;                // can be 192 or 128

                byte[] initVectorBytes;
                byte[] saltValueBytes;
                byte[] cipherTextBytes;

                cipherTextBytes = Convert.FromBase64String(cipherText);
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                byte[] keyBytes;
                keyBytes = password.GetBytes(keySize / 8);
                //RijndaelManaged symmetricKey = new RijndaelManaged();
                Aes symmetricKey = Aes.Create();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes;
                plainTextBytes = cipherTextBytes;
                int decryptedByteCount;
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            }
            catch (Exception ex)
            {
                if (handle) { }
                //Logger.Write($"Decrypt('{cipherText}')", ex, "Framework");

                else
                    throw new Exception($"{(ex != null ? ex.Message : "")}", ex);
            }

            return plainText;
        }

    }
}
