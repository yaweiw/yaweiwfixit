using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLTest4
{
    public static class TaskHelper
    {
        public static async Task test()
        {
            await Task.Delay(1);
        }
        public static async Task ForEachInParallelAsync<T>(this IEnumerable<T> source, Func<T, Task> body, int maxParallelism)
        {
            if (body == null)
            {
                throw new ArgumentNullException("body");
            }

            using (var semaphore = new SemaphoreSlim(maxParallelism))
            {
                // warning "access to disposed closure" around "semaphore" could be ignored as it is inside Task.WhenAll
                await Task.WhenAll(from s in source select ForEachCoreAsync(body, semaphore, s));
            }
        }
        private static async Task ForEachCoreAsync<T>(Func<T, Task> body, SemaphoreSlim semaphore, T s)
        {
            await semaphore.WaitAsync();
            try
            {
                await body(s);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
    class Program
    {
        async static Task call(int[] a1, int[] a2)
        {
            await a1.ForEachInParallelAsync(
                async arr1Item =>
                {
                    await a2.ForEachInParallelAsync(
                        async arr2Item =>
                        {
                            if (arr1Item == arr2Item)
                            {
                                await Task.Delay(1);
                                Console.WriteLine(arr1Item + ":" + arr2Item);
                            }
                        },
                        16);
                },
                16);
        }
        static void Main(string[] args)
        {
            int[] arr1 = new int[10] { 5, 4, 3, 6, 87, 13, 0, 89, 9, 10 };
            int[] arr2 = new int[2] { 89, 87 };
            Task t = call(arr1, arr2);
            t.Wait();
            Console.ReadLine();
        }
    }
}
