namespace HomeWork1
{
    public class Task1
    {

        public static double Factorial(double numeric) //Task 1
        {
            if (numeric < 0)
            {
                return double.NaN;
            }
            if (numeric == 0)
            {
                return 1;
            }
            double result = 1;
            for (int i = 1; i < numeric; i++)
            {
                result *= i;
            }
            return result;
        }

        public static int Fibonnaci(int order) // Task 2
        {
            if (order is 0 or 1)
            {
                return order;
            }

            int firstN = 0;
            int secondN = 1;
            int temp;

            for (int i = 0; i < order; i++)
            {
                temp = secondN;
                secondN += firstN;
                firstN = temp;
            }
            return secondN;
        }

        public static void QuickSort<T>(T[] arg, int begin, int end) where T : IComparable<T>  // Task 3
        {
            // опорный элемент
            T baseElement = arg[(end - begin) / 2 + begin];
            int i = begin;
            int j = end;
            T temp;

            while (i <= j)
            {
                while (arg[i].CompareTo(baseElement) < 0 && end >= i)
                {
                    i++;
                }
                while (arg[j].CompareTo(baseElement) > 0 && begin <= j)
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
            if (j > begin)
            {
                QuickSort(arg, begin, j);
            }
            if (i < end)
            {
                QuickSort(arg, i, end);
            }
        }

        public static void ShowArrayHelix(double[,] mas) // Task 4 
        {
            int length = mas.GetLength(0) - 1;
            int line = length / 2;
            int column = line;

            for (var i = length / 2; i >= 0; --i)
            {
                while (column > i)
                {
                    Console.Write($"{mas[line, column]}  ");
                    --column;
                }

                while (line < length - i)
                {
                    Console.Write($"{mas[line, column]}  ");
                    ++line;
                }

                while (column < length - i)
                {
                    Console.Write($"{mas[line, column]}  ");
                    ++column;
                }

                while (line >= i)
                {
                    Console.Write($"{mas[line, column]}  ");
                    --line;
                }
            }
        }

        public static void ShowArray(double[,] arr)
        {
            for (int i = 0; i <= arr.GetUpperBound(0); i++)
            {
                for (int k = 0; k <= arr.GetUpperBound(1); k++)
                {
                    Console.Write(arr[i, k] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void MatrixSort<T>(T[,] arr) where T : IComparable<T>
        {

            for (int columN = 0; columN < arr.GetUpperBound(1); columN++)
            {
                for (int i = 0; i < arr.GetUpperBound(0); i++)
                {
                    //Bubble sort, хотел бы просто ссылки на элементы запихать в другой массив и сорт, но как-то шарп не хочет
                    for (int j = 0; j < arr.GetUpperBound(0) - i; j++)
                    {
                        //Если элемент массива с индексом j больше следующего за ним элемента
                        if (arr[j, columN].CompareTo(arr[j + 1, columN]) > 0)
                        {
                            //Меняем местами элемент массива с индексом j и следующий за ним
                            (arr[j + 1, columN], arr[j, columN]) = (arr[j, columN], arr[j + 1, columN]);
                        }
                    }

                }
            }
        }



    }
}
