using ConsoleApp;
namespace ConsoleAppTests
{
    [TestClass]
    public class LazyFactoryTest
    {
        [TestMethod]
        public void SingleThreadingTest()
        {
            Assert.AreEqual(LazyFactory<int>.CreateSinglethreadingLazy(() => 5).Get() , 5);
            ILazy<int> lazytest;
            lazytest = LazyFactory<int>.CreateSinglethreadingLazy(() => 7);
            Assert.AreEqual(lazytest.Get(), 7);
            Assert.AreEqual(lazytest.Get(), 7);
        }

        [TestMethod]
        public void MultiThreadingTest()
        {
            Assert.AreEqual(LazyFactory<int>.CreateMultithreadingLazy(() => 5).Get(), 5);
            ILazy<int> lazytest;
            lazytest = LazyFactory<int>.CreateMultithreadingLazy(() => 7);

            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine($"The thread {i} started");
                Thread myThread = new(() => Assert.AreEqual(7, lazytest.Get()));
                myThread.Name = $"Поток {i}";   // устанавливаем имя для каждого потока
                myThread.Start();
                Console.WriteLine($"The thread {i} end");
            }
        }
    }
}
