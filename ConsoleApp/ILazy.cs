namespace ConsoleApp
{
    public interface ILazy<T>
    {
        T Get();
    }
}
