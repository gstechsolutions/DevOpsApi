using System.Security.Cryptography;
using System.Text;

namespace DevOpsApi.core.api.Services
{
    public static class AESHelper
    {
        private const string EncryptionKey = "your-encryption-key"; // Must match the Angular key

        private static readonly string Key = "1234567890123456"; // Must match Angular
        private static readonly string IV = "1234567890123456";  // Must match Angular

        public static string DecryptPassword(string encryptedPassword)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream(encryptedBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }            
        }        
    }
}
