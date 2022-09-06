namespace ConsoleApp
{
    public class LazySinglethreading<T> : ILazy<T>
    {
        private T _value;
        private bool _valueExist = false;
        private Func<T>? _function;

        public LazySinglethreading(Func<T>? function)
        {
            _function = function;
        }

        public T Get()
        {
            if (!_valueExist)
            {
                _valueExist = true;

                if (_function == null)
                {
                    return default(T);
                }

                _value = _function();
            }

            return _value;
        }
    }
}
