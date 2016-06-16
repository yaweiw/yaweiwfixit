using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace RedisTest
{
    public class BlobTest
    {
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;
        private Dictionary<string, string> _currentDict;

        public BlobTest(string dir)
        {
            _storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            _blobClient = _storageAccount.CreateCloudBlobClient();
            
            using (StreamReader r = new StreamReader(dir + @"\metadata80k_m.json"))
            {
                string json = r.ReadToEnd();
                _currentDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            CreateFolders(dir);
            ClearFiles(dir);
        }

        private void CreateFolders(string dir)
        {
            if(!Directory.Exists(dir + @"\cache"))
            {
                Directory.CreateDirectory(dir + @"\cache");
            }
            if (!Directory.Exists(dir + @"\cache\output"))
            {
                Directory.CreateDirectory(dir + @"\cache\output");
            }
        }

        private void ClearFiles(string dir)
        {
            string[] files = new string[] {
                dir + @"\cache\metadata1k.json",
                dir + @"\cache\metadata1k.zip",
                dir + @"\cache\metadata10k.json",
                dir + @"\cache\metadata10k.zip",
                dir + @"\cache\metadata80k.json",
                dir + @"\cache\metadata80k.zip",
                dir + @"\cache\output.zip",
                dir + @"\cache\output\output.zip",
                dir + @"\cache\output\output.json"
            };
            foreach(var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }

        private async Task DownloadAndUnzip(string blobName, string dir)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference("cache");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName + ".zip");
            using (Stream stream = File.Create(dir + @"\cache\" + blobName + ".zip"))
            {
                await blockBlob.DownloadToStreamAsync(stream);
            }
            ZipFile.ExtractToDirectory(dir + @"\cache\" + blobName + ".zip", dir + @"\cache\");
            return;
        }

        private async Task ZipAndUpload(string dir)
        {
            string output = JsonConvert.SerializeObject(_currentDict);
            File.AppendAllText(dir + @"\cache\output\output.json", output);
            ZipFile.CreateFromDirectory(dir + @"\cache\output\", dir + @"\cache\output.zip");
            CloudBlobContainer container = _blobClient.GetContainerReference("cache");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("output.zip");
            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = File.OpenRead(dir + @"\cache\output.zip"))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
            return;
        }

        private IEnumerable<KeyValuePair<string,string>> Diff(Dictionary<string,string> dicOne, Dictionary<string,string> dicTwo)
        {
            return dicOne.Except(dicTwo).Concat(dicTwo.Except(dicOne));
        }

        public void Test(string metadatafile, string dir)
        {
            ClearFiles(dir);
            Stopwatch stopWatch0 = new Stopwatch();
            stopWatch0.Start();
            Task t0 = DownloadAndUnzip(metadatafile,dir);
            t0.Wait();
            stopWatch0.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts0 = stopWatch0.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime0 = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts0.Hours, ts0.Minutes, ts0.Seconds,
                ts0.Milliseconds / 10);
            Console.WriteLine("DownloadAndUnzip: " + elapsedTime0);

            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            Dictionary<string, string> previousDict;
            using (StreamReader r = new StreamReader(dir + @"\" + metadatafile + ".json"))
            {
                string json = r.ReadToEnd();
                previousDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            var diff = Diff(previousDict, _currentDict);
            stopWatch1.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts1 = stopWatch1.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime1 = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts1.Hours, ts1.Minutes, ts1.Seconds,
                ts1.Milliseconds / 10);
            Console.WriteLine("Diff: " + elapsedTime1);
            //
            //publishing work
            //
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            Task z = ZipAndUpload(dir);
            z.Wait();
            stopWatch2.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts2 = stopWatch2.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime2 = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts2.Hours, ts2.Minutes, ts2.Seconds,
                ts2.Milliseconds / 10);
            Console.WriteLine("ZipAndUpload: " + elapsedTime2);
            Console.ReadLine();
        }
    }
}
