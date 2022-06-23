using System;
namespace ConsoleApp
{
	public class IncorrectRemoveException<T> : ArgumentException
	{
        public T Value { get; }
        public IncorrectRemoveException(string message, T value)
            : base(message)
        {
            Value = value;
        }
    }
}

