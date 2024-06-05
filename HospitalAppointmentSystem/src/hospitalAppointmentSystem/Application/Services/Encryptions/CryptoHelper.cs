using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Encryptions;
public  class CryptoHelper
{

 // SİNEM kodları
//GetBytes metodlarındaki şifrelendirmeler değitirilmemli değiştinde birda çözme işlemi yapmaz.

    //0123456789ABCDEF0123456789ABCDEF
    // 32 byte key for AES-256 encryption
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("7785,.Hospital88*#!12??-lhm++0=-"); // şifreleme yaparken kullanılan anahtar. bu anahtar değişirse şifreleme yapılan veriler çözlemez.
    // 16 byte IV for AES
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("dhjaks(+**'o4d78"); // şifreleme yaparken kullanılan anahtar.

    public static string Encrypt(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        try
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return cipherText;
        }
    }
}
