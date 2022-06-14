namespace ConsoleApp
{
    public class HashTable<T> where T : class? // Task 2

    {
        //private double rehash_size = 0.75;
        private const int defaultSize = 16;
        private int size = 0;
        private T[][]? array;

        public HashTable()
        {
            size = defaultSize;
            InitializeArray(ref array, size);
        }

        // Инициализирует массив
        private void InitializeArray(ref T[][] arr, int initialSize)
        {
            arr = new T[initialSize][];
            for (int step = 0; step < initialSize; step++)
            {
                arr[step] = new T[16];
            }
        }

        public int HashFunction(T? value)
        {
            int simpleNumber = 31;
            int result = 0;
            string stringValue = Convert.ToString(value);
            for (int i = 0; i < Convert.ToString(value).Length; i++)
            {
                result += (int)Math.Pow(simpleNumber, stringValue.Length - 1 - i) * stringValue[i]; //Подсчет хеш-функции
            }
            return result;
        }

        // увеличивает размер массива до нужного ключа
        private void Resize(int newSize)
        {
            int previousSize = size;
            size = newSize;
            T[][] array2 = new T[size][];
            InitializeArray(ref array2, size);

            for (int nodeNumber = 0; nodeNumber < previousSize; nodeNumber++)
            {
                (array2[nodeNumber], array[nodeNumber]) = (array[nodeNumber], array2[nodeNumber]);
            }
            array = array2;
        }

        public void Add(T value)
        {
            int position = HashFunction(value);

            if (position + 1 > size)
            {
                Resize(position + 1);
                size = position + 1;
            }

            int elementNumber = 0;

            while (array[position][elementNumber] is not null)
            {
                if (elementNumber == 15)
                {
                    throw new StackOverflowException();
                }
                elementNumber++;
            }

            array[position][elementNumber] = value;
        }

        //Укузатель на положение элемента в ноде 
        private int GetValuePointer(T value)
        {
            int result = -1;
            int position = HashFunction(value);

            if (position > size)
            {
                return result;
            }

            int elementNumber = 0;

            while (array[position][elementNumber] == null || !array[position][elementNumber].Equals(value))
            {
                if (elementNumber == 15)
                {
                    return result;
                }

                elementNumber++;
            }

            return elementNumber;
        }

        // проверка существования элемента
        public bool Find(T value)
        {
            int elementNumber = GetValuePointer(value);

            if (elementNumber == -1)
            {
                return false;
            }

            return true;
        }

        // удаление элемента
        public bool Remove(T value) // потому что в родительском определен метод удаляющий по позиции
        {
            int position = HashFunction(value);
            int elementNumber = GetValuePointer(value);

            if (elementNumber == -1)
            {
                return false;
            }

            array[position][elementNumber] = null;

            return true;
        }


        public void ShowInfo()
        {
            Console.WriteLine("Current table has:");
            for (int nodeNumber = 0; nodeNumber < size; nodeNumber++)
            {
                for (int positionInNode = 0; positionInNode < defaultSize; positionInNode++)
                {
                    if (array[nodeNumber][positionInNode] != null)
                    {
                        Console.WriteLine("The value {0}, with position {1}", array[nodeNumber][positionInNode], nodeNumber);
                    }
                }
            }
        }
    }
}