using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace mahoa
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("nhap password: ");
            //string password=Console.ReadLine();
            crypt Horse = new crypt();
            if (args[2] == "enc")
            {
                string text1 = File.ReadAllText(args[0]);
                string encryptstring = Horse.startencrypt(args[1], text1);
                File.WriteAllText(@"decrypt.txt", encryptstring);
            }
            else if (args[2] == "dec")
            {
                string text = File.ReadAllText(args[0]);
                string decryptstring = Horse.startdecrypt(args[1], text);
                File.WriteAllText(@"plaintext.txt", decryptstring);
            }
        }

    }
}
