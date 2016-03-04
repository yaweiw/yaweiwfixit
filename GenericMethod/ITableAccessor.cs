using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorageAccessorGeneric
{
    public interface ITableAccessor<T>
        where T: class, ITableEntity, new()
    {
        string TableName { get; }
        Task<bool> InsertOrUpdateAsync(T entity);

    }
}
