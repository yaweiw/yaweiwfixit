using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;
using TableStorageAccessorGeneric;
using System.Net;

namespace TableStorageAccessorGeneric
{
    internal class TableAccessor<T> : ITableAccessor<T> where T : class, IMyTableEntity, new()
    {
        private TableAccessorFactory tableAccessorFactory;

        private readonly string tableName;

        protected CloudTable Table { get; private set; }

        public TableAccessor(TableAccessorFactory _factor, Type _typeinfo)
        {
            tableAccessorFactory = _factor;
            tableName = _typeinfo.Name;
            Table = StorageHelper.tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();
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

        private async Task<bool> ModifyAsync(Func<TableEntityAdapter<T>, TableOperation> insertOrReplace, T entity, OperationContext operationContext = null)
        {
            try
            {
                var result = await Table.ExecuteAsync(insertOrReplace(new TableEntityAdapter<T>(entity)), null, operationContext);
                if (result.HttpStatusCode >= 200 && result.HttpStatusCode < 300) return true;
                else return false;
            }
            catch (StorageException ex)
            {
                var innerEx = ex.InnerException as WebException;
                if (innerEx != null)
                {
                    var resp = innerEx.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        // Conflict: 409, PreconditionFailed: 412
                        if (resp.StatusCode == HttpStatusCode.Conflict || resp.StatusCode == HttpStatusCode.PreconditionFailed)
                        {
                            resp.Dispose();
                            return false;
                        }
                    }
                }
                throw;
            }
        }
    }
}