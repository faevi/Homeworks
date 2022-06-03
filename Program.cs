    // See https://aka.ms/new-console-template for more information
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    namespace Giraffe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetDay(2));

            Console.ReadLine();
        }
        static string GetDay(int dayNum)
        {
            string dayName;
            switch(dayNum)
            {
                case 0:
                    dayName = "Sunday";
                    break;
                case 1:
                    dayName = "Monday";
                        break;
                default:
                    dayName = "Invalid Number";
                    break;
            }
            return dayName;

        }
    }
}


