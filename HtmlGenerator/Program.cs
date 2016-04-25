using HtmlGenerator;
using System.IO;
using Microsoft.Azure; // Namespace for CloudConfigurationManager 
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;

namespace HtmlGeneratorCS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Retrieve storage account from connection string.
            StorageCredentials cre = new StorageCredentials("yaweiw", "cshgCWFn9Z3drf06xj2zdTYLxR3pxZhuBHsm0HsWCl7PadTfS+xABmS/br0TJDQBaNPLxVFpE3b+6dpIbktq0A==");
            CloudStorageAccount storageAccount = new CloudStorageAccount(cre, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");
            blockBlob.Properties.ContentType = "application/octet-stream";

            MemoryStream ms = (MemoryStream)GetStream();
            //using (var sr = new StreamReader(ms as MemoryStream))
            //{
            var sr = new StreamReader(ms);
            File.WriteAllText(@"c:\blah.dat", sr.ReadToEnd());
            //}
            blockBlob.UploadFromStream(ms);
        }
        private static Stream GetStream()
        {
            ReportGenerator rg = new ReportGenerator();
            Stream memorystream = new MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(memorystream);
            writer.WriteLine(rg.HtmlHeaderBeginTag.ToString());
            writer.WriteLine(rg.HtmlBodyBeginTag.ToString());
            writer.WriteLine(rg.HtmlReportHeader.ToString());
            writer.WriteLine(rg.HtmlBuildSummary.ToString());
            writer.WriteLine(rg.HtmlBuildFiles.ToString());
            writer.WriteLine(rg.HtmlBuildDetails.ToString());
            writer.WriteLine(rg.HtmlBodyEndTag.ToString());
            writer.WriteLine(rg.HtmlRawLog.ToString());
            writer.WriteLine(rg.HtmlHeaderEndTag.ToString());
            memorystream.Seek(0, SeekOrigin.Begin);
            return memorystream;
        }
    }

}


