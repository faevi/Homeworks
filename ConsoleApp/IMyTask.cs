namespace ConsoleApp
{
    public interface IMyTask<TResult>
    {
        public bool IsComplete
        {
            get;
        }

        public TResult Result
        {
            get;
        }

        IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> function);
    }
}
