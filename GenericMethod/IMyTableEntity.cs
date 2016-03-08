using System;

namespace TableStorageAccessorGeneric
{
    public interface IMyTableEntity
    {
        string PartitionKey { get; set; }

        string RowKey { get; set; }

        DateTimeOffset Timestamp { get; set; }

        string ETag { get; set; }
    }
}