
namespace HomeWork2
{
    // Task 1
    //Класс нод
    public class Node<T>
    {
        public Node(T? Field)
        {
            field = Field;
        }
        public T? field
        {
            get;
            set;
        }
        public Node<T>? nextElement
        {
            get;
            set;
        }
        public bool state = true;
    }

    //сам связанный список
    public class newLinkedList<T>
    {
        Node<T>? begin;
        Node<T>? end;
        public int nodesCount = 0;

        public int emptyCount = 0;

        public virtual void Add(T element, int nodeNumber)
        {
            Node<T> newNode = new Node<T>(element);
            //Console.WriteLine("Begin/end number: {0}", nodeNumber);

            if (nodeNumber > nodesCount) // если элемент за списком
            {
                if (begin == null)
                {
                    begin = new Node<T>(default);
                    begin.state = false;
                    nodesCount++;
                    emptyCount++;
                }

                Node<T> temp = begin;

                for (int step = 0; step < nodeNumber - 1; step++) // создаем пустышки для всех значений меньше
                {
                    if (temp.nextElement == null)
                    {
                        temp.nextElement = new Node<T>(default);
                        temp.nextElement.state = false;
                        emptyCount++;
                        nodesCount++;
                    }

                    temp = temp.nextElement;
                    // помечаем как пустышки
                }
                end = newNode;
                temp.nextElement = end;
                nodesCount++;
            }
            else if (nodeNumber == nodesCount) // если номер первый/последний
            {
                //Console.WriteLine("Begin/end number: {0}", nodeNumber);
                if (nodeNumber == 0) // замена первого
                {
                    begin = newNode;
                    nodesCount++;
                }
                else
                {
                    if (end == null) // если конец до этого не был задан
                    {
                        //Console.WriteLine("Adding end of list!");
                        end = newNode;
                        begin!.nextElement = end;
                        //Console.WriteLine("Adding end of list, after begin.next.field: {0}", begin.nextElement.field);
                        nodesCount++;
                    }
                    else // замена последнего элемента
                    {
                        Node<T> current = begin!;

                        //Console.WriteLine("Change end, but before begin field: {1}, end field {2}, begin.next field: {3}", nodeNumber, begin.field, end.field, begin.nextElement.field);

                        for (int step = 0; step < nodeNumber - 1; step++) // поиск нужного положения
                        {
                            //Console.WriteLine("Moving on list at step: {0}, current field: {1}, current next field {2}", step, current.field, current.nextElement.field);
                            current = current.nextElement!;
                        }
                        end = newNode;
                        current.nextElement = end;
                        nodesCount++;
                        //Console.WriteLine("Node number: {0}, begin field: {1}, end field {2}, Node field: {3}", nodeNumber, begin.field, end.field, newNode.field);

                    }
                }
            }
            else // если номер уже существует в списке
            {
                //Console.WriteLine("The Node with number {0} is already exist!", nodeNumber);
                Node<T> previos = null;
                Node<T> current = begin!;

                if (nodeNumber == 0) // если это первый элемент
                {
                    newNode.nextElement = begin!.nextElement;
                    if (begin.state == false) //если до этого был пустышкой
                    {
                        emptyCount--;
                    }
                    begin = newNode;
                    return;
                }

                for (int step = 0; step < nodeNumber; step++) // поиск нужного положения
                {
                    //Console.WriteLine("Moving on list in step: {0} to node with field: {1}", step, current.nextElement.field);
                    previos = current!;
                    current = current.nextElement!;
                }
                //замена ноды на уже существующую
                previos.nextElement = newNode;
                newNode.nextElement = current.nextElement;
                if (current.state == false) // если до этого элемент был пустышкой
                {
                    emptyCount--;
                }
            }
        }

        public virtual T? GetValue(int nodeNumber)
        {
            if (nodeNumber > nodesCount)
            {
                Console.WriteLine("Incorrect position: {0}", nodeNumber);
                return default;
            }
            else
            {
                Node<T> temp = begin!;

                for (int position = 0; position < nodeNumber; position++)
                {
                    temp = temp.nextElement!;
                }

                if (temp.state == true)
                {
                    return temp.field;
                }
                else
                {
                    Console.WriteLine("Empty positinon: {0}, nothing here!", nodeNumber);
                    return default;
                }
            }
        }

        public virtual Node<T> GetNode(int nodeNumber)
        {
            if (nodeNumber > nodesCount)
            {
                Console.WriteLine("Incorrect position: {0}", nodeNumber);
                return default;
            }
            else
            {
                Node<T> temp = begin!;

                for (int position = 0; position < nodeNumber; position++)
                {
                    temp = temp.nextElement!;
                }
                return temp;
            }
        }

        public virtual void SetValue(T element, int nodeNumber)
        {
            if (nodeNumber > nodesCount)
            {
                Console.WriteLine("Incorrect position: {0}", nodeNumber);
                return;
            }
            else
            {
                Node<T> newNode = new Node<T>(element);
                Node<T> previos = null;
                Node<T> current = begin!;

                if (nodeNumber == 0) // если это первый элемент
                {
                    newNode.nextElement = begin!.nextElement;
                    begin = newNode;
                    return;
                }

                for (int step = 0; step < nodeNumber; step++) // поиск нужного положения
                {
                    //Console.WriteLine("Moving on list in step: {0} to node with field: {1}", step, current.nextElement.field);
                    previos = current!;
                    current = current.nextElement!;
                }
                if (current.state == false)
                {
                    emptyCount--;
                }
                //замена ноды на уже существующую
                previos!.nextElement = newNode;
                newNode.nextElement = current.nextElement;
            }
        }

        // пусть нам надо, чтобы после удаления все ноды сохраняли свои позиции,
        // поэтому вместо полного удаления их состояние меняется на false
        public virtual bool Remove(int nodeNumber)
        {
            if (begin == null)
            {
                Console.WriteLine("Empty table!");
                return false;
            }

            Node<T> current = begin;

            if (nodeNumber > nodesCount)
            {
                Console.WriteLine("Incorrect position: {0}", nodeNumber);
                return false;
            }

            for (int step = 0; step < nodeNumber; step++)
            {
                current = current.nextElement!;
            }
            //Console.WriteLine(current.state+ " " + nodeNumber+ " "+emptyCount);
            if (current.state == true)
            {
                //Console.WriteLine("BOOM!");
                emptyCount++;
            }
            current.state = false;
            current.field = default;
            //Console.WriteLine(current.state + " " + nodeNumber + " " + emptyCount);
            return true;
        }

        public static void ShowLinnkedList(newLinkedList<T> list) // Вывод всех нод листа
        {
            if (list.begin == null)
            {
                return;
            }

            Console.WriteLine("This List has {0} elements", list.nodesCount);

            Node<T> temp = list.begin;
            for (int node = 0; node < list.nodesCount; node++)
            {
                if (temp == null)
                {
                    continue;
                }
                Console.WriteLine("Node: {0}, with field: {1}, and state: {2}", node, temp.field, temp.state);
                temp = temp.nextElement!;
            }
        }

        public static void ShowInfo(newLinkedList<T> list) //узнать пустой или размер если не пустой
        {
            bool isEmpty = list.nodesCount > 0 ? false : true;
            if (isEmpty)
            {
                Console.WriteLine("The linked list is empty!");
            }
            else
            {
                Console.WriteLine("Current list has {0} elements, {1} elements is real, {2} elements is empty",
                    list.nodesCount, list.nodesCount - list.emptyCount, list.emptyCount);
            }
        }
    }



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


    //Task 3
    class StackCalculator
    {
        private static int operationStackSize = 0;
        private static int outStackSize = 0;
        private static Operation[] operationStack;
        private static string[] outStack;
        private static bool isNextSign = true;
        private static bool isPreviousOperation = false;

        private struct Operation
        {
            public char operationName { get; set; }
            public int operationPriority { get; set; }
            public Operation(char name)
            {
                this.operationName = name;
                this.operationPriority = name switch
                {
                    '/' => 1,
                    '*' => 1,
                    '+' => 2,
                    '-' => 2,
                    '(' => 0,
                    ')' => 0
                };
            }
        }

        //private enum Operation
        //{
        //    Add,
        //    Subtract,
        //    Multiply,
        //    Divide
        //}
        
        private static double DoOperation(double x, double y, Operation op)
        {
            double result = op.operationName switch
            {
                '/' => x / y,
                '*' => x * y,
                '+' => x + y,
                '-' => x - y
            };
            return result;
        }

        private static void ChangeStack(Operation op)
        {
            //Console.WriteLine("Im trying to add operation: {0}, outStackSize: {1}, opStack Size: {2}", op.operationName, outStackSize, operationStackSize);
            //outStackSize++;
            switch (op.operationName)
            {
                case '(':
                    ifOpenScobe(op);
                    break;
                case ')':
                    ifCloseScobe();
                    break;
                case '+':
                    ifBinary(op);
                    break;
                case '-':
                    ifBinary(op);
                    break;
                case '/':
                    ifBinary(op);
                    break;
                case '*':
                    ifBinary(op);
                    break;
            }
        }

        private static void ifOpenScobe(in Operation ch)
        {
            operationStack[operationStackSize] = ch;
            operationStackSize++;
            isNextSign = true;
            isPreviousOperation = false;
        }

        private static void ifCloseScobe()
        {
            outStackSize++;
            while (operationStack[operationStackSize - 1].operationName != '(')
            {
                outStack[outStackSize] += operationStack[operationStackSize - 1].operationName;
                outStackSize++;


                if (operationStackSize - 1 == 0)
                {
                    Console.WriteLine("Wrong formula format!");
                    return;
                }

                operationStackSize--;
            }
            isPreviousOperation = false;
            operationStackSize--;
            isNextSign = false;
        }

        private static void ifBinary(Operation op) // Вот это поебень я сделал
        {
            //ShowOpStack();
            //ShowOutStack();
            if (operationStackSize == 0 || operationStack[operationStackSize - 1].operationPriority == 0)
            {
                operationStack[operationStackSize] = op;
                if (outStackSize != 0 && operationStackSize == 0)
                {
                    outStackSize--;
                }
                outStackSize++;
            }
            else if (operationStack[operationStackSize - 1].operationPriority > op.operationPriority)
            {
                operationStack[operationStackSize] = op;
            }
            else
            {
                operationStackSize--;

                while (operationStack[operationStackSize].operationPriority <= op.operationPriority ||
                    operationStack[operationStackSize].operationPriority != 0)
                {
                    //Console.WriteLine("Im on opStack position: {0}, meanwhile outStackPosition: {1}", operationStackSize, outStackSize);
                    outStackSize++;
                    outStack[outStackSize] += operationStack[operationStackSize].operationName;
                    outStackSize++;

                    if (operationStackSize == 0)
                    {
                        break;
                    }
                    operationStackSize--;
                }

                operationStack[operationStackSize] = op;
            }
            isNextSign = false;
            isPreviousOperation = true;
            operationStackSize++;
            //ShowOpStack();
            //ShowOutStack();
        }


        private static void CreateStack(in string formula)
        {
            operationStack = new Operation[formula.Length];
            outStack = new string[formula.Length];
            operationStackSize = 0;
            outStackSize = 0;
            isNextSign = true;
            isPreviousOperation = false;

            char[] CH = formula.ToCharArray();

            foreach (char ch in CH) //алгоритм преобразованя из инфиксной нотации в обратную польскую запись
            {
                //Console.WriteLine("Trying to Add: {0}", ch);
                int bar;
                if (int.TryParse(ch.ToString(), out bar) || ch == '.' || isNextSign && ch != '(' && ch != ')')
                {
                    outStack[outStackSize] += ch;
                    isNextSign = false;
                    continue;
                }
                ChangeStack(new Operation(ch));
            }
            // перенос оставшегося стека операций в выохдной стек
            //ShowOpStack();
            //ShowOutStack();
            for (int stackNode = operationStackSize; stackNode > 0; --stackNode)
            {
                outStackSize++;
                if (!isPreviousOperation)
                {
                    outStackSize--;
                    isPreviousOperation = true;
                }
                outStack[outStackSize] += operationStack[stackNode - 1].operationName;
            }
            outStackSize++;
            //ShowOpStack();
            //ShowOutStack();
        }

        public static string DoCallculation(string formula)
        {
            CreateStack(formula);
            int step = 0;
            double firstValue = 0;
            double secondValue = 0;

            while (step + 2 < outStackSize)
            {
                double bar;
                if (!double.TryParse(outStack[step + 2], out bar))
                {
                    if (double.TryParse(outStack[step], out firstValue)
                        && double.TryParse(outStack[step + 1], out secondValue))
                    {
                        char charOperation = outStack[step + 2].ToCharArray()[0];
                        Operation op = new Operation(charOperation);
                        outStack[step + 2] = Convert.ToString(DoOperation(firstValue, secondValue, op));
                    }
                }
                step++;
            }
            return outStack[step + 1];
        }

        public static void ShowOutStack()
        {
            Console.WriteLine("Out Stack with {0} elemnts: ", outStackSize);
            for (int step = 0; step < outStackSize; step++)
            {
                Console.WriteLine(outStack[step] + " ");
            }
            //Console.WriteLine();
        }

        public static void ShowOpStack()
        {
            Console.WriteLine("Op Stack with {0} elemnts: ", operationStackSize);
            for (int step = 0; step < operationStackSize; step++)
            {
                Console.WriteLine(operationStack[step].operationName + " ");
            }
            //Console.WriteLine();
        }        
    }
}
