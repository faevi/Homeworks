namespace ConsoleApp
{
    //сам связанный список
    public class NewLinkedList<T>
    {
        protected class Node<T>
        {
            public Node(T? value)
            {
                field = value;
            }
            public T? field { get; set; }
            public Node<T>? NextElement { get; set; }
        }

        private Node<T>? begin;

        public int nodesCount = 0;

        public virtual void Add(T element, int nodeNumber)
        {
            Node<T> newNode = new Node<T>(element);

            if (begin == null || nodeNumber == 0) //если нод не было или меняем 0
            {
                if (nodeNumber == 0 && nodesCount > 1) // если меняем 0 в списке более чем с 1 элементом
                {
                    Node<T> temp = begin;
                    newNode.NextElement = temp;
                }
                begin = newNode;

            }
            else if (IsNodeExist(nodeNumber)) // если нода с таким номер существует
            {
                Node<T> temp = GetNode(nodeNumber - 1);
                newNode.NextElement = temp.NextElement;
                temp.NextElement = newNode;
            }
            else // если хотим поставить на послнее место
            {
                GetNode(nodesCount - 1).NextElement = newNode;
            }

            nodesCount++;
        }

        // найти значение ноды
        public virtual T? GetValue(int nodeNumber) => GetNode(nodeNumber).field;

        //Проверить существует ли нода
        private bool IsNodeExist(int nodeNumber)
        {
            if (nodeNumber >= nodesCount || nodeNumber < 0)
            {
                return false;
            }
            return true;
        }

        // найти ноду
        protected virtual Node<T> GetNode(int nodeNumber)
        {
            if (!IsNodeExist(nodeNumber))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                Node<T> temp = begin!;

                for (int position = 0; position < nodeNumber; position++)
                {
                    temp = temp.NextElement!;
                }
                return temp;
            }
        }

        // изменить значение ноды
        public virtual void SetValue(T element, int nodeNumber)
        {
            GetNode(nodeNumber - 1).NextElement.field = element;
        }

        // удалить ноду
        public virtual bool Remove(int nodeNumber)
        {
            if (nodeNumber == nodesCount - 1) // елси элемент последний
            {
                Node<T> temp = GetNode(nodeNumber - 1);
                temp.NextElement = null;
                nodesCount--;
                return true;
            }
            else if (IsNodeExist(nodeNumber)) // если элемент есть в списке
            {
                if (nodeNumber == 0) // замена начала
                {
                    begin = begin.NextElement;
                    nodesCount--;
                    return true;
                }

                Node<T> temp = GetNode(nodeNumber - 1);
                temp.NextElement = temp.NextElement.NextElement;
                nodesCount--;
                return true;
            }
            return false;
        }

        public static void ShowLinkedList(NewLinkedList<T> list) // Вывод всех нод листа
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
                Console.WriteLine("Node: {0}, with field: {1}", node, temp.field);
                temp = temp.NextElement!;
            }
        }

        public static void ShowInfo(NewLinkedList<T> list) //узнать пустой или размер если не пустой
        {
            bool isEmpty = list.nodesCount > 0 ? false : true;
            if (isEmpty)
            {
                Console.WriteLine("The linked list is empty!");
            }
            else
            {
                Console.WriteLine("Current list has {0} elements: ", list.nodesCount);
            }
        }
    }
}