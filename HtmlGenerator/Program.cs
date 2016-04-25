using HtmlGenerator;
using System.IO;
using Microsoft.Azure; // Namespace for CloudConfigurationManager 
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.WindowsAzure.Storage.Auth;

namespace HtmlGeneratorCS
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = @"DefaultEndpointsProtocol=https;AccountName=yaweiw;AccountKey=account-key";
            // Retrieve storage account from connection string.
            StorageCrentials cre = new StorageCrentials();
            CloudStorageAccount storageAccount = new CloudStorageAccount();

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("photos");

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
                var blob = GetBlockBlobReference(path);
                blob.Properties.ContentType = contentType ?? "application/octet-stream";
                await blob.UploadFromStreamAsync(source);

            }
        }
    }
}
