namespace TableStorageAccessorGeneric
{
    public interface ITableEntity
    {
        string PartitionKey { get; set; }

        string RowKey { get; set; }

        string ETag { get; set; }
    }
}