using System.Threading;

namespace ConsoleApp
{
    public class MyTask<TResult> : IMyTask<TResult>
    {
        private Func<TResult> _function;
        private TResult _result;
        private ManualResetEvent _waitHandle = new ManualResetEvent(false);
        private MyThreadPool _threadPool;
        private Queue<Action> _taskQueue;
        private bool _isComplete = false;
        private object locker = new object();

        public MyTask(Func<TResult> function, MyThreadPool threadPool)
        {
            _threadPool = threadPool;
            _taskQueue = new Queue<Action>();
            _function = function;
        }

        public bool IsComplete 
        {
            get => _isComplete;
            set => _isComplete = value;
        }

        public TResult Result
        {
            get
            {
                _waitHandle.WaitOne();
                return _result;
            }
        }

        public void DoTask()
        {
            lock (locker)
            {
                try
                {
                    _result = _function();
                }
                catch (Exception ex)
                {
                    throw new AggregateException(ex);
                }
                finally
                {
                    IsComplete = true;
                    _waitHandle.Set();
                    while(_taskQueue.Count() != 0)
                    {
                        _threadPool.AddAction(_taskQueue.Dequeue());
                    }
                }
            }            
        }

        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> function)
        {
            MyTask<TNewResult> newTask = new MyTask<TNewResult>(() => function(_result), _threadPool);
            
            if (IsComplete)
            {
                _threadPool.AddTask(newTask);
            } else
            {
                _taskQueue.Enqueue(() => newTask.DoTask());
            }
    
            return newTask;            
        }
    }
}
