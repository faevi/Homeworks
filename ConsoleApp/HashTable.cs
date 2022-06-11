namespace HomeWork2
{
    public class HashTable<T> : newLinkedList<T> // Task 2

    {
        //Т.к. вместо массива у меня мой слепленный связанный список, то рехеш и ресайз не нужен))0))0)
        //private newLinkedList<T> list = new newLinkedList<T>();
        //private double rehash_size = 0.75;
        //private int defaultSize = 8;

        int HashFunction1(T value, int table_size)
        {
            {
                return HashFunction(value, table_size, table_size - 1);
            }
        }

        int HashFunction2(T value, int table_size)
        {
            {
                return HashFunction(value, table_size, table_size + 1);
            }
        }


        public int HashFunction(T? value, int table_size, int key)
        {
            int p = 31; //Простое число
            int rez = 0; //Результат вычисления
            string stringValue = Convert.ToString(value);
            for (int i = 0; i < Convert.ToString(value).Length; i++)
            {
                rez += (int)Math.Pow(p, stringValue.Length - 1 - i) * (int)(stringValue[i]);//Подсчет хеш-функции
            }
            return rez;
        }

        public bool Find(T value)
        {
            int h1 = HashFunction1(value, nodesCount); // значение, отвечающее за начальную позицию
            int h2 = HashFunction2(value, nodesCount); // значение, ответственное за "шаг" по таблице
            int i = 0;

            while (h1 < nodesCount && i < nodesCount)
            {
                Node<T> temp = GetNode(h1); // Чтобы два раза по листу не бегать в след if

                //Console.WriteLine("temp.field: {0}, temp position: {1}, value: {2}, temp state: {3}", temp.field, h1, value, temp.state);
                if (temp.field != null && temp.field.Equals(value) && temp.state)
                {
                    return true; // такой элемент есть
                }

                h1 = (h1 + h2) % nodesCount;
                i++;
                //Console.WriteLine("h1: {0}, nodesCount: {1}", h1, list.nodesCount);
            }
            return false;
        }

        public new bool Remove(T value) // потому что в родительском определен метод удаляющий по позиции
        {
            int h1 = HashFunction1(value, nodesCount); // значение, отвечающее за начальную позицию
            int h2 = HashFunction2(value, nodesCount); // значение, ответственное за "шаг" по таблице

            while (h1 < nodesCount)
            {
                Node<T> temp = GetNode(h1); // Чтобы два раза по листу не бегать в след if

                if (temp.field!.Equals(value) && temp.state)
                {
                    temp.state = false;
                    emptyCount++;
                    return true; // такой элемент есть
                }

                h1 = (h1 + h2) % nodesCount;
            }

            return false;
        }

        public bool Add(T value)
        {
            int h1 = HashFunction1(value, nodesCount); // значение, отвечающее за начальную позицию
            int h2 = HashFunction2(value, nodesCount); // значение, ответственное за "шаг" по таблице

            while (h1 < nodesCount)
            {
                Node<T> temp = GetNode(h1); // Чтобы два раза по листу не бегать в след if

                if (!temp.state)
                {
                    Add(value, h1);
                    return true; // такой элемент есть
                }

                h1 = (h1 + h2) % nodesCount;
            }

            Add(value, h1);
            return true;
        }

        public static void ShowInfo(HashTable<T> table)
        {
            bool isEmpty = table.nodesCount > 0 ? false : true;
            if (isEmpty)
            {
                Console.WriteLine("The table is empty!");
            }
            else
            {
                Console.WriteLine("Current table has {0} elements, {1} elements is real, {2} elements is empty",
                    table.nodesCount, table.nodesCount - table.emptyCount, table.emptyCount);
            }
            ShowLinnkedList(table);
        }
    }
}

