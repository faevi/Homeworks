
namespace ConsoleApp
{
    class Program
    {





        static void Main(string[] args)
        {
            /*
            Random random = new Random();
            int N = 9;
            //Task 1
            Console.WriteLine("\n\nTask 1 Factorial \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(Task1.Factorial(i) + " ");
            }
            //Task 2
            Console.WriteLine("\n \nTask 2 Fibbonaci \n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(Task1.Fibonnaci(i) + " ");
            }
            //Task 3
            Console.WriteLine("\n \n Task 3 Sort\n");
            double[] a3 = new double[N];
            for (int i = 0; i < N; i++)
            {
                a3[i] = random.Next(0, 100);
                Console.Write(a3[i] + " ");
            }
            Task1.QuickSort(a3, 0, N - 1);
            Console.WriteLine("\n QuickSort\n");
            for (int i = 0; i < N; i++)
            {
                Console.Write(a3[i] + " ");
            }

            //Task 4
            Console.WriteLine("\n \n Task 4 HelixShow\n");

            double[,] a4 = new double[N, N];


            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    a4[i, j] = random.Next(100);

                }

            }
            Task1.ShowArray(a4);
            Task1.ShowArrayHelix(a4);



            //Task 5
            Console.WriteLine("\n\n\nTask 5 Sort first table\n");

            Task1.MatrixSort(a4);
            Task1.ShowArray(a4);
            */
            //HashTable<string> table = new HashTable<string>();
            //for (int i = 0; i < 10; i++)
            //{
            //    //Console.WriteLine("Add element: {0}", i);
            //    table.Add(Convert.ToString(i));
            //}
            //HashTable<string>.ShowInfo(table);
            //Console.WriteLine(table.Find("0"));
            //Console.WriteLine(table.Remove("0"));            
            //Console.WriteLine(table.Find("0"));
            //Console.WriteLine(table.Find("3"));
            //Console.WriteLine(table.Remove("3"));
            //Console.WriteLine(table.Find("3"));
            //Console.WriteLine(table.Remove("100"));
            //Console.WriteLine(table.Find("100"));
            //HashTable<string>.ShowInfo(table);


            //Task 1
            newLinkedList<int> list = new newLinkedList<int>();
            
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Add in list element: {0}", i);
                list.Add(i,i);
            }
            
            list.Add(50, 15);
            newLinkedList<int>.ShowInfo(list);

            //Task 2

            HashTable<string> table = new HashTable<string>();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Add in table element: {0}", i);
                table.Add(Convert.ToString(i));
            }
            HashTable<string>.ShowInfo(table);

            //Task 3
            Console.WriteLine(StackCalculator.DoCallculation("1+3-(5-10)*4"));
            Console.WriteLine(StackCalculator.DoCallculation("(-1157+24)/17*3"));
            //StackCalculator.ShowOutStack();
            //StackCalculator.CreateStack("-1.5+(-3-(5-10)*4)");
            //StackCalculator.ShowOutStack();
            //StackCalculator.CreateStack("(1+3)-(5-10)*4");
            //StackCalculator.ShowOutStack();

        }



    }
}