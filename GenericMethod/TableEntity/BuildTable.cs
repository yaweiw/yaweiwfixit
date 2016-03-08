using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorageAccessorGeneric
{
    public class BuildTable : IMyTableEntity
    {
        public BuildTable()
        {
            PartitionKey = @"43fdea24-2538-3bc0-7c1e-539b16ad9e4e";
            RowKey = @"201602020906316936-live";
            ETag = @"buildtableetagvalue";
            Timestamp = DateTimeOffset.Now;
            Attr = @"attribute";
        }

        public string ETag { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Attr { get; set; }
    }
}
