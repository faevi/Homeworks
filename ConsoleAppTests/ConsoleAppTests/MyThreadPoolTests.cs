using ConsoleApp;

namespace ConsoleAppTests
{
    [TestClass]
    public class MyThreadPoolTests
    {
        [TestMethod]
        public void FirstMyThreadPoolTest()
        {
            List<MyTask<int>> taskList = new List<MyTask<int>>();
            MyThreadPool testPool = new MyThreadPool(4);
            for (int i = 0; i < 100; i++)
            {
                MyTask<int> testTask = new MyTask<int>(() => i*i, testPool);
                taskList.Add(testTask);
            }

            for (int i = 0; i < 100; i++)
            {
                MyTask<int> testTask = new MyTask<int>(() => i*i, testPool);
                taskList.Add(testTask);
            }


        }
    }
}
