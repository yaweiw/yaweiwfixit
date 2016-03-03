using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace GetMd5HashedGuid
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">GitRepositoryUrl</param>
        /// <param name="algorithm">MD5CryptoServiceProvider</param>
        /// <param name="encoding">Encoding.UTF8</param>
        /// <returns></returns>
        public static byte[] ComputeHash(string input, HashAlgorithm algorithm, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return algorithm.ComputeHash(encoding.GetBytes(input));
        }

        static void Main(string[] args)
        {
            //string input = @"https://github.com/yaweiw/yaweiwrepo";
            string input = @"${8507779}--{GitHub}";
            byte[] hashedBytes;
            Encoding encoding = null;
            using (var csp = new MD5CryptoServiceProvider())
            {
                hashedBytes = ComputeHash(input, csp, encoding ?? Encoding.UTF8);
            }

            Guid output = new Guid(hashedBytes);

            string[] lines = { input, output.ToString()};
            
            File.WriteAllLines(@"e:\temp\output.txt", lines);
            Console.WriteLine(@"output to e:\temp\output.txt");

        }
    }
}
