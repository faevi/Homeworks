using System;
namespace ConsoleApp
{
	public class DublicateException<T> : ArgumentOutOfRangeException
    {
        public T Value { get; }
        public DublicateException(string message, T value)
            : base(message)
        {
            Value = value;
        }
    }
}

