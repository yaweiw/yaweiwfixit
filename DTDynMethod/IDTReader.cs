using System;

namespace DTDynMethod
{
    public interface IDTReader
    {
        string PartitionKey { get; set; }

        string RowKey { get; set; }

        DateTimeOffset Timestamp { get; set; }

        string ETag { get; set; }
    }
}