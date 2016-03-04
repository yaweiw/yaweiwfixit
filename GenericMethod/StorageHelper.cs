using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TableStorageAccessorGeneric
{
     internal static class StorageHelper
    {
        static CloudStorageAccount storageAccount;

        public readonly static CloudTableClient tableClient;

        static StorageHelper()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            tableClient = storageAccount.CreateCloudTableClient();
        }
    }
}