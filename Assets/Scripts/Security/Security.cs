//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Security.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

/// <summary>Manage the security of data game.</summary>
public class Security : MonoBehaviour
{
    /// <summary>Encrypts the specified path file.</summary>
    /// <param name="pathFile">The path file.</param>
    /// <param name="data">The data.</param>
    public static void Encrypt(string pathFile, string data)
    {
        string key = "dgwn`SCN7342!/(&2-.MSDUOUQsdbasd3e435" + pathFile;
        byte[] stringToEncrypt = UTF8Encoding.UTF8.GetBytes(data);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform desEncrypt = tdes.CreateEncryptor();
        byte[] stringEncrypt = desEncrypt.TransformFinalBlock(stringToEncrypt, 0, stringToEncrypt.Length);

        File.WriteAllText(pathFile, Convert.ToBase64String(stringEncrypt));
        tdes.Clear();
    }

    /// <summary>Decrypts the specified path file.</summary>
    /// <param name="pathFile">The path file.</param>
    /// <returns>Return string decrypted</returns>
    public static string Decrypt(string pathFile)
    {
        string key = "dgwn`SCN7342!/(&2-.MSDUOUQsdbasd3e435" + pathFile;
        string result = string.Empty;
        string stringEncrypt = File.ReadAllText(pathFile);

        byte[] arrayEncrypt = Convert.FromBase64String(stringEncrypt);

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        ICryptoTransform desEncrypt = tdes.CreateDecryptor();
        result = UTF8Encoding.UTF8.GetString(desEncrypt.TransformFinalBlock(arrayEncrypt, 0, arrayEncrypt.Length));
        tdes.Clear();

        return result;
    }
}