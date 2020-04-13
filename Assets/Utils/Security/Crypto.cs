//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Crypto.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>Manage message security.</summary>
    public static class Crypto
    {
        /// <summary>Default key</summary>
        private const string DefaultKey = "kkGz=<{2CcQ^KrGT7TaD9dF,Xz5/+>CwfB4A}.V>RA;3NUz,7~(n8CP*U6JbTr)!pV_p;AJ9TezdBs`]_L/#)4qw:cxBh5<vjCc+D%h~$G(&=h%}Cm{wddgK2c:D(ds{";

        /// <summary>Encrypts the specified data with a default key.</summary>
        /// <param name="data">The data.</param>
        /// <returns>Return data encrypted</returns>
        public static string Encrypt(string data)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            byte[] key = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(DefaultKey));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform desEncrypt = tdes.CreateEncryptor();
            byte[] stringEncrypt = desEncrypt.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);

            tdes.Clear();
            hashmd5.Clear();

            return Convert.ToBase64String(stringEncrypt);
        }

        /// <summary>Encrypts the specified data with a key</summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns>Return data encrypted</returns>
        public static string Encrypt(string data, string key)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            byte[] dataArray = Encoding.UTF8.GetBytes(data);
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform desEncrypt = tdes.CreateEncryptor();
            byte[] stringEncrypt = desEncrypt.TransformFinalBlock(dataArray, 0, dataArray.Length);

            tdes.Clear();
            hashmd5.Clear();

            return Convert.ToBase64String(stringEncrypt);
        }

        /// <summary>Decrypts the specified data with default key</summary>
        /// <param name="data">The data.</param>
        /// <returns>Return data decrypted</returns>
        public static string Decrypt(string data)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            byte[] dataEncrypted = Convert.FromBase64String(data);
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(DefaultKey));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform desEncrypt = tdes.CreateDecryptor();
            byte[] stringDecrypted = desEncrypt.TransformFinalBlock(dataEncrypted, 0, dataEncrypted.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Encoding.UTF8.GetString(stringDecrypted);
        }

        /// <summary>Decrypts the specified data with a key</summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns>Return data decrypted</returns>
        public static string Decrypt(string data, string key)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            byte[] dataEncrypted = Convert.FromBase64String(data);
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform desEncrypt = tdes.CreateDecryptor();
            byte[] stringDecrypted = desEncrypt.TransformFinalBlock(dataEncrypted, 0, dataEncrypted.Length);

            hashmd5.Clear();
            tdes.Clear();

            return Encoding.UTF8.GetString(stringDecrypted);
        }
    }
}