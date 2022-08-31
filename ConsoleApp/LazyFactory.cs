using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class LazyFactory<T>
    {
        public static ILazy<T> CreateMultithreadingLazy<T>(Func<T> supplier)
        {
            return new LazyMultithreading<T>(supplier);
        }
    }
}
