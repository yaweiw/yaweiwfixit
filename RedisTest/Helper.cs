using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    public class Helper
    {
        public static Dictionary<string, string> Dict = new Dictionary<string, string>();
        static string ComputeMd5HashForString(string source)
        {
            byte[] hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            return Encoding.Default.GetString(hash);
        }

        public static string GenerateKey()
        {
            return Guid.NewGuid().ToString();
        }

        public static string ComputeHash(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void CreateMetadataFile(string filePath, Dictionary<string, string> dict)
        {
            File.AppendAllText(filePath, "{");
            int index = 0;
            foreach(KeyValuePair<string,string> pair in dict)
            {
                if (index++ != dict.Count() - 1)
                {
                    File.AppendAllText(filePath, $"\"{pair.Key}\": \"{pair.Value}\",");
                }
                else
                {
                    File.AppendAllText(filePath, $"\"{pair.Key}\": \"{pair.Value}\"");
                }
            }
            File.AppendAllText(filePath, "}");
        }

        public static void LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }

    }
}
