namespace ConsoleApp
{
    public class LazyFactory<T>
    {
        public static ILazy<T> CreateMultithreadingLazy<T>(Func<T> supplier)
        {
            return new LazyMultithreading<T>(supplier);
        }

        public static ILazy<T> CreateSinglethreadingLazy<T>(Func<T> supplier)
        {
            return new LazySinglethreading<T>(supplier);
        }
    }
}
