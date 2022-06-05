namespace Task1;

class Program
{
    static double Factorial(double numeric) //Task 1
    {
        if (numeric % 1 != 0 || numeric < 0)
        {
            return double.NaN;
        }
        if (numeric == 0)
        {
            return 1;
        }
        double result=1;
        for (int i = 1; i <= numeric; i++)
        {
            result *= i;
        }
        return result;
    }

    static int Fibonnaci(int order) // Task 2
    {
        if (order is 0 or 1)
        {
            return order;
        }
        return Fibonnaci(order-1)+Fibonnaci(order-2);
    }

    static void QuickSort<T>(T[] arg, int begin, int end) where T : IComparable<T>  // Task 3
    {
        // опорный элемент
        T baseEl = arg[(end - begin) / 2 + begin];
        int i = begin, j = end;
        T temp;

        while(i <= j)
        {
            while (arg[i].CompareTo(baseEl) < 0 && end >= i)
            {
                i++;
            }
            while (arg[j].CompareTo(baseEl) > 0 && begin <= j)
            {
                j--;
            }

            //меняем местами
            if (i <= j)
            {
                temp = arg[i];
                arg[i] = arg[j];
                arg[j] = temp;
                i++;
                j--;
            }
        } 

        //рекурсия
        if (j > begin) QuickSort( arg, begin, j);
        if (i < end) QuickSort( arg, i, end);
    }

    static void ShowArrayHelix(double[,] arr) // Task 4 
    {
        //find center
        int yPos = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(arr.GetUpperBound(0)) / 2.0));
        int xPos = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(arr.GetUpperBound(1)) / 2.0));

        Console.Write("\n The Array: " + arr[xPos, yPos]+" ");

        int circle = 0;
        int direction = 1;

        while (circle < arr.GetUpperBound(0))
        {            
            for ( int i = 1; i <= circle+1; i++)
            {
                yPos += direction ;
                Console.Write(arr[xPos, yPos]+ " ");
            }
            for (int i = 1; i <= circle + 1; i++)
            {
                xPos += direction ;
                Console.Write(arr[xPos, yPos] + " ");
            }
            direction *= -1;
            circle++;

            //Последняя строка
            if (circle == arr.GetUpperBound(0))
            {
                for (int i = 1; i <= circle ; i++)
                {
                    yPos += direction;
                    Console.Write(arr[xPos, yPos] + " ");
                }
            }
        }
    }

    static void ShowArray(double[,] arr)
    {
        for (int i =0;i <= arr.GetUpperBound(0);i++)
        {
            for(int k =0; k <= arr.GetUpperBound(1); k++)
            {
                Console.Write(arr[i, k]+ " ");
            }
            Console.WriteLine();
        }
    }

    static void MatrixSort<T>(T[,] arr) where T : IComparable<T>
    {
        
        for (int columN = 0; columN < arr.GetUpperBound(1); columN++)
        {
            for (int i = 0; i < arr.GetUpperBound(0); i++)
            {
                //Bubble sort, хотел бы просто ссылки на элементы запихать в другой массив и сорт, но как-то шарп не хочет
                for (int j = 0; j < arr.GetUpperBound(0) - i; j++)
                {
                    //Если элемент массива с индексом j больше следующего за ним элемента
                    if (arr[j,columN].CompareTo(arr[j + 1, columN]) > 0 )
                    {
                        //Меняем местами элемент массива с индексом j и следующий за ним
                        (arr[j + 1, columN], arr[j, columN]) = (arr[j, columN], arr[j + 1, columN]);
                    }
                }

            }
        }
    }


    static void Main(string[] args)
    {
        Random random = new Random();
        int N = 9;
        //Task 1
        Console.WriteLine("\n\nTask 1 Factorial \n");
        for (int i = 0; i < N; i++)
        {
            Console.Write(Factorial(i)+ " ");
        }
        //Task 2
        Console.WriteLine("\n \nTask 2 Fibbonaci \n");
        for (int i = 0; i < N; i++)
        {
            Console.Write(Fibonnaci(i)+" ");
        }
        //Task 3
        Console.WriteLine("\n \n Task 3 Sort\n");
        double[] a3 = new double[N];
        for (int i=0; i < N; i++)
        {
            a3[i] = random.Next(0, 100);
            Console.Write(a3[i]+" ");
        }
        QuickSort(a3,0,N-1);
        Console.WriteLine("\n QuickSort\n");
        for (int i = 0; i < N; i++)
        {
            Console.Write(a3[i]+" ");
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
        ShowArray(a4);
        ShowArrayHelix(a4);
        

        
        //Task 5
        Console.WriteLine("\n\n\nTask 5 Sort first table\n");

        MatrixSort(a4);
        ShowArray(a4);


    }



}
