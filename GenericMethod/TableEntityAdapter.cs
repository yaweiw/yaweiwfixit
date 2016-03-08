using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorageAccessorGeneric
{
    public class TableEntityAdapter<T> : ITableEntity
        where T : class, IMyTableEntity, new()
    {
        public T Inner { get; private set; }

        public TableEntityAdapter()
        {
            Inner = new T();
        }

        public TableEntityAdapter(T inner)
        {
            if (inner == null) throw new ArgumentNullException("inner is null");
            Inner = inner;
        }

        public string PartitionKey
        {
            get
            {
                return Inner.PartitionKey;
            }

            set
            {
                Inner.PartitionKey = value;
            }
        }

        public string RowKey
        {
            get
            {
                return Inner.RowKey;
            }

            set
            {
                Inner.RowKey = value;
            }
        }

        public DateTimeOffset Timestamp
        {
            get
            {
                return Inner.Timestamp;
            }

            set
            {
                Inner.Timestamp = value;
            }
        }

        public string ETag
        {
            get
            {
                return Inner.ETag;
            }

            set
            {
                Inner.ETag = value;
            }
        }

        /// <inheritdoc />
        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            TableEntity.ReadUserObject(Inner, properties, operationContext);

            ReadValues(properties, operationContext);
        }

        /// <inheritdoc />
        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var properties = TableEntity.WriteUserObject(Inner, operationContext);

            WriteValues(properties, operationContext);

            return properties;
        }

        protected virtual void ReadValues(
            IDictionary<string, EntityProperty> properties,
            OperationContext operationContext)
        {
            //foreach (var item in properties)
            //{
            //    operationContext.UserHeaders.Add(item.Key,item.Value.StringValue);
            //}
        }

        protected virtual void WriteValues(
            IDictionary<string, EntityProperty> properties,
            OperationContext operationContext)
        {
            //foreach(var item in operationContext.UserHeaders)
            //{
            //    properties.Add(item.Key, new EntityProperty(item.Value));
            //}
        }
    }
}
