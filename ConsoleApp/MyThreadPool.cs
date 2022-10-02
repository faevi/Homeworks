using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp
{
    public class MyThreadPool
    {
        private BlockingCollection<Action> _taskQueue;
        private CancellationTokenSource source = new CancellationTokenSource();

        public void AddTask<TResult>(MyTask<TResult> task)
        {
            if (source.Token.IsCancellationRequested)
            {
                throw new InvalidOperationException("ShutDown");
            }

            _taskQueue.Add(() => task.DoTask(), source.Token);
        }

        public MyThreadPool(int threadsNumber)
        {
            _taskQueue = new BlockingCollection<Action>();
            CreateThreadPool(threadsNumber);
        }

        private void CreateThreadPool(int threadsNumber)
        {
            for (int thread = 0; thread < threadsNumber; thread++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            _taskQueue.Take().Invoke();

                            if (source.IsCancellationRequested && _taskQueue.Count == 0)
                            {
                                break;
                            }
                        }
                        catch (AggregateException)
                        {

                        }
                    }
                }
                ).Start();
            }
        }

        public void AddAction(Action action)
        {
            if (source.Token.IsCancellationRequested)
            {
                throw new InvalidOperationException("ShutDown");
            }

            _taskQueue.Add(action, source.Token);
        }

        public void ShutDown()
        {
            source.Cancel();
        }
    }
}
