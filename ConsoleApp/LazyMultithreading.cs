namespace ConsoleApp
{
    public class LazyMultithreading<T> : ILazy<T>
    {
        private T _value;
        private bool _valueExist = false;
        private Func<T>? _function;

        public LazyMultithreading(Func<T>? function)
        {
            _function = function;
        }

        public T Get()
        {
            AutoResetEvent waitHandler = new AutoResetEvent(true);

            if (!_valueExist)
            {
                waitHandler.WaitOne();
                _valueExist = true;

                if (_function == null)
                {
                    return default(T);
                }

                _value = _function();
                waitHandler.Set();
            }

            return _value;
        }
    }
}
