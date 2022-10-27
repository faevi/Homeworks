using ConsoleApp;

namespace ConsoleAppTests
{
    [TestClass]
    public class MyThreadPoolTests
    {
        [TestMethod]
        public void MyTaskTest()
        {
            MyThreadPool pool = new MyThreadPool(1);
            MyTask<int> task = new MyTask<int>(() => 5*5, pool);
            task.DoTask();
            Assert.AreEqual(task.Result, 25);
            pool.ShutDown();
        }

        [TestMethod]
        public void FirstMyThreadPoolTest()
        {
            MyTask<int>[] taskList = new MyTask<int>[100];
            MyThreadPool testPool = new MyThreadPool(4);

            for (int i = 0; i < 100; i++)
            {
                int number = i;
                MyTask<int> testTask = new MyTask<int>(() => number*number, testPool);
                testPool.AddTask(testTask);
                taskList[i] = testTask;
            }
            
            Thread.Sleep(300);
            Console.WriteLine($"Hi, task List {taskList.Length} and {taskList[0].IsComplete}");

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"i*i = {i*i}, and task result is {taskList[i].Result}");
                Assert.AreEqual(taskList[i].Result, i*i);
            }
            testPool.ShutDown();
        }

        [TestMethod]
        public void MyThreadPoolContinueWithTest()
        {
            MyTask<int>[] taskList = new MyTask<int>[100];
            MyTask<int>[] newTaskList = new MyTask<int>[300];
            MyThreadPool testPool = new MyThreadPool(4);

            for (int i = 0; i < 100; i++)
            {
                int number = i;
                MyTask<int> testTask = new MyTask<int>(() => number*number, testPool);
                testPool.AddTask(testTask);
                taskList[i] = testTask;
                MyTask<int> newTestTask = (MyTask<int>)testTask.ContinueWith<int>((int firstResult) => firstResult*4);
                MyTask<int> newSecondTestTask = (MyTask<int>)testTask.ContinueWith<int>((int firstResult) => firstResult*5);
                MyTask<int> newNewTestTask = (MyTask<int>)newTestTask.ContinueWith<int>((int firstResult) => firstResult*4);
                newTaskList[i*3] = newTestTask;
                newTaskList[i*3+1] = newSecondTestTask;
                newTaskList[i*3+2] = newNewTestTask;
            }

            
            Thread.Sleep(300);

            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(taskList[i].Result, i*i);
                Assert.AreEqual(newTaskList[i*3].Result, taskList[i].Result*4);
                Assert.AreEqual(newTaskList[i*3+1].Result, taskList[i].Result*5);
                Assert.AreEqual(newTaskList[i*3+2].Result, newTaskList[i*3].Result*4);
            } 
            testPool.ShutDown();
        }

        [TestMethod]
        public void MyThreadPoolTest_TryingAddTaskAfterShutDown()
        {
            MyThreadPool testPool = new MyThreadPool(4);
            testPool.ShutDown();
            try
            {
                int number = 5;
                MyTask<int> testTask = new MyTask<int>(() => number*number, testPool);
                testPool.AddTask(testTask);
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
