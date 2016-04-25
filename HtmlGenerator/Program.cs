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
            StorageCredentials cre = new StorageCredentials("accname","key");
            CloudStorageAccount storageAccount = new CloudStorageAccount(cre, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            ReportGenerator rg = new ReportGenerator();
            using (Stream memorystream = new MemoryStream())
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(memorystream))
            {
                writer.WriteLine(rg.HtmlHeaderBeginTag.ToString());
                writer.WriteLine(rg.HtmlBodyBeginTag.ToString());
                writer.WriteLine(rg.HtmlReportHeader.ToString());
                writer.WriteLine(rg.HtmlBuildSummary.ToString());
                writer.WriteLine(rg.HtmlBuildFiles.ToString());
                writer.WriteLine(rg.HtmlBuildDetails.ToString());
                writer.WriteLine(rg.HtmlBodyEndTag.ToString());
                writer.WriteLine(rg.HtmlRawLog.ToString());
                writer.WriteLine(rg.HtmlHeaderEndTag.ToString());

                blockBlob.Properties.ContentType = "application/octet-stream";
                memorystream.Seek(0, SeekOrigin.Begin);
                blockBlob.UploadFromStream(memorystream);
            }
        }
    }
}
