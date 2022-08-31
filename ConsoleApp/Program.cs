namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new int[] { 1, 5, 2, 4, 7, 2, 4, 9, 3, 6, 5 };
            var threads = new Thread[3];
            var chunkSize = array.Length / threads.Length + 1;
            var results = new int[threads.Length];
            for (var i = 0; i < threads.Length; ++i)
            {
                var localI = i;
                threads[i] = new Thread(() => {
                    for (var j = localI * chunkSize; j < (localI + 1) * chunkSize && j < array.Length; ++j)
                        results[localI] += array[j];
                });
            }
            foreach (var thread in threads)
                thread.Start();
            foreach (var thread in threads)
                thread.Join();
            var result = 0;
            foreach (var subResult in results)
                result += subResult;
            Console.WriteLine($"Result = {result}");
        }
    }
}
