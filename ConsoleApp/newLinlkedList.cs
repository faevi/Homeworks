namespace ConsoleApp
{
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
        public bool state = true; // существует ли такой элемент
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
}

