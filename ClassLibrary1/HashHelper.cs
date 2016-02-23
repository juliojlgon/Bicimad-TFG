using System;
using System.Security.Cryptography;
using System.Text;

namespace Bicimad.Helpers
{
    public static class HashHelper
    {
        private const string Itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static bool CheckHash(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash)) return false;

            if (hash.Length == 34) return (HashCryptPrivate(Encoding.ASCII.GetBytes(password), hash, Itoa64) == hash);

            return false;
        }

        public static string Hash(string password)
        {
            var random = Encoding.ASCII.GetBytes(new Random().Next(100000, 999999).ToString());
            var hash = HashCryptPrivate(Encoding.ASCII.GetBytes(password), HashGensaltPrivate(random, Itoa64), Itoa64);

            return hash.Length == 34 ? hash : SMd5(password);
        }

        private static string HashCryptPrivate(byte[] password, string genSalt, string itoa64)
        {
            var output = "*";
            var md5 = new MD5CryptoServiceProvider();

            if (!genSalt.StartsWith("$P$")) return output;

            var countLog2 = itoa64.IndexOf(genSalt[3]);
            if (countLog2 < 7 || countLog2 > 30) return output;

            var count = 1 << countLog2;
            var salt = Encoding.ASCII.GetBytes(genSalt.Substring(4, 8));

            if (salt.Length != 8) return output;

            var hash = md5.ComputeHash(Combine(salt, password));

            do
            {
                hash = md5.ComputeHash(Combine(hash, password));
            } while (count-- > 1);

            output = genSalt.Substring(0, 12);
            output += HashEncode64(hash, 16, itoa64);

            return output;
        }

        private static byte[] Combine(byte[] b1, byte[] b2)
        {
            var retVal = new byte[b1.Length + b2.Length];
            Array.Copy(b1, 0, retVal, 0, b1.Length);
            Array.Copy(b2, 0, retVal, b1.Length, b2.Length);
            return retVal;
        }

        private static string HashEncode64(byte[] input, int count, string itoa64)
        {
            var output = "";
            var i = 0;

            do
            {
                int value = input[i++];
                output += itoa64[value & 0x3f];

                if (i < count) value |= input[i] << 8;
                output += itoa64[(value >> 6) & 0x3f];
                if (i++ >= count)
                    break;

                if (i < count) value |= input[i] << 16;
                output += itoa64[(value >> 12) & 0x3f];
                if (i++ >= count)
                    break;

                output += itoa64[(value >> 18) & 0x3f];

            } while (i < count);

            return output;
        }

        private static string HashGensaltPrivate(byte[] input, string itoa64)
        {
            const int iterationCountLog2 = 8;

            string output = "$P$";
            output += itoa64[Math.Min(iterationCountLog2 + 5, 30)];
            output += HashEncode64(input, 6, itoa64);

            return output;
        }

        private static string SMd5(string password, bool raw = false)
        {
            var md5 = new MD5CryptoServiceProvider();
            return raw ?
                Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(password))) :
                BitConverter.ToString(md5.ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", "");
        }
    }
}
