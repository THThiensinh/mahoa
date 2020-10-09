using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.IO;
using System;
class crypt
{
    public string startencrypt(string password, string text)
    {
        //Console.WriteLine("start");
        byte[] rawPlaintext = System.Text.Encoding.UTF8.GetBytes(text);
        using (Aes aes = Aes.Create())
        {
            //aes.Padding = PaddingMode.PKCS7;
            //aes.KeySize = 128;          // in bits
            //aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
            aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV
                                          // Should set Key and IV here.  Good approach: derive them from 
                                          // a password via Cryptography.Rfc2898DeriveBytes 

            byte[] temp = Encoding.UTF8.GetBytes(password);

            //Console.WriteLine(temp);
            HashAlgorithm hash = MD5.Create();
            aes.Key = hash.ComputeHash(temp);
            byte[] cipherText = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                }

                cipherText = ms.ToArray();
            }
            string a = Convert.ToBase64String(cipherText);
            Console.WriteLine(a);
            return a;
        }
;
    }
    public string startdecrypt(string password, string text)
    {
        using (Aes aes = Aes.Create())
        {
            //aes.Padding = PaddingMode.PKCS7;
            //aes.KeySize = 128;          // in bits
            //aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
            aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV
                                          // Should set Key and IV here.  Good approach: derive them from 
                                          // a password via Cryptography.Rfc2898DeriveBytes 

            byte[] temp = Encoding.UTF8.GetBytes(password);

            //Console.WriteLine(temp);
            HashAlgorithm hash = MD5.Create();
            aes.Key = hash.ComputeHash(temp);
            byte[] cipherText = Convert.FromBase64String(text);
            byte[] plainText = null;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                    }

                    plainText = ms.ToArray();
                    string s = System.Text.Encoding.UTF8.GetString(plainText);
                    Console.WriteLine(s);
                    return s;
                }
                catch (CryptographicException)
                {
                    Console.WriteLine("wrong password");
                    return "";
                }
            }
        }
    }
}