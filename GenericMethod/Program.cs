using System;
using System.Globalization;

namespace TableStorageAccessorGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            TableAccessorFactory factory = new TableAccessorFactory();
            ITableAccessor<BuildTable> buildtableaccessor = factory.CreateTableAccessor<BuildTable>();
            Console.WriteLine(buildtableaccessor.TableName);
            ITableAccessor<CommitTable> committableaccessor = factory.CreateTableAccessor<CommitTable>();
            Console.WriteLine(committableaccessor.TableName);
            ITableAccessor<PushTable> pushtableaccessor = factory.CreateTableAccessor<PushTable>();
            Console.WriteLine(pushtableaccessor.TableName);

            BuildTable buildtableinstance = new BuildTable();
            buildtableaccessor.InsertOrUpdateAsync(buildtableinstance).Wait();
        }
    }
}
