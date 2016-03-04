using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;

namespace TableStorageAccessorGeneric
{
    internal class TableAccessor<T> : ITableAccessor<T> where T : class, ITableEntity, new()
    {
        private TableAccessorFactory tableAccessorFactory;

        private readonly string tableName;

        public TableAccessor(TableAccessorFactory _factor, Type _typeinfo)
        {
            tableAccessorFactory = _factor;
            tableName = _typeinfo.Name;
        }

        public string TableName
        {
            get
            {
                return tableName;
            }

        }

        public Task<bool> InsertOrUpdateAsync(T entity)
        {
            // Refer to http://azure.microsoft.com/blog/2014/09/08/managing-concurrency-in-microsoft-azure-storage-2/
            // "Insert or Replace Entity" operations do not send an ETag value to table service, so manually send If-Match header to table service.
            return ModifyAsync(
                TableOperation.InsertOrReplace,
                entity,
                new OperationContext
                {
                    UserHeaders = new Dictionary<string, string>
                    {
                        { "If-Match", "W/\"datetime'2016-03-04T07%3A17%3A58.8Z'\"" }
                    }
                });
        }

        private Task<bool> ModifyAsync(Func<Microsoft.WindowsAzure.Storage.Table.ITableEntity, TableOperation> insertOrReplace, T entity, OperationContext operationContext)
        {
            StorageHelper.tableClient
        }
    }
}