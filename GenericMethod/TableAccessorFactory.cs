using TableStorageAccessorGeneric;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorageAccessorGeneric
{
    public class TableAccessorFactory
    {
        public ITableAccessor<T> CreateTableAccessor<T>()
            where T : class, IMyTableEntity, new()
        {
            return new TableAccessor<T>(this,typeof(T));
        }
    }
}
