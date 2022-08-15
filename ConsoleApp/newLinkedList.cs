using System.Collections;

namespace ConsoleApp
{
    //сам связанный список
    public class NewLinkedList<T> : IList<T>
    {        
        protected class Node<T>
        {
            public Node(T? value)
            {
                Field = value;
            }
            public T? Field { get; set; }
            public Node<T>? NextElement { get; set; }
        }

        private Node<T>? _begin;

        private int nodesCount = 0;

        public virtual void Clear()
        {
            _begin = null;
        }
        public virtual int Count => nodesCount;

        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        T IList<T>.this[int index] 
        {
            get => GetValue(index); 
            set => Insert(index,value); 
        }

        public virtual bool Contains(T value)
        {
            try
            {
                return GetNodeByValue(value).Field.Equals(value);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public virtual int IndexOf(T value)
        {
           int index = 0;

            if (_begin is null)
            {
                throw new NullReferenceException();
            }

            Node<T> temp = _begin!;

            while (!temp.Field.Equals(value))
            {
                if (temp.NextElement is null)
                {
                    throw new IncorrectRemoveException<T>($"No argument with value {value}", value);
                }
                index++;
                temp = temp.NextElement!;
            }
            return index;
        }

        public virtual void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (_begin == null) 
            {
                _begin = newNode;
            }
            else 
            {
                GetNodeByNumber(nodesCount - 1).NextElement = newNode;
            }

            nodesCount++;
        }

        public virtual void Insert(int nodeNumber, T element)
        {
            Node<T> newNode = new Node<T>(element);

            if (_begin == null || nodeNumber == 0) //если нод не было или меняем 0
            {
                if (nodeNumber == 0 && nodesCount > 1) // если меняем 0 в списке более чем с 1 элементом
                {
                    Node<T> temp = _begin;
                    newNode.NextElement = temp;
                }
                _begin = newNode;
            }
            else if (IsNodeExist(nodeNumber)) // если нода с таким номер существует
            {
                Node<T> temp = GetNodeByNumber(nodeNumber - 1);
                newNode.NextElement = temp.NextElement;
                temp.NextElement = newNode;
            }
            else // если хотим поставить на послнее место
            {
                GetNodeByNumber(nodesCount - 1).NextElement = newNode;
            }

            nodesCount++;
        }

        // найти значение ноды
        public virtual T? GetValue(int nodeNumber) => GetNodeByNumber(nodeNumber).Field;

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
        protected virtual Node<T> GetNodeByNumber(int nodeNumber)
        {
            if (!IsNodeExist(nodeNumber))
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                Node<T> temp = _begin!;

                for (int position = 0; position < nodeNumber; position++)
                {
                    temp = temp.NextElement!;
                }
                return temp;
            }
        }

        protected virtual Node<T> GetNodeByValue(T value)
        {
            if (_begin is null)
            {
                throw new NullReferenceException();
            }

            Node<T> temp = _begin!;

            while (!temp.Field.Equals(value))
            {
                if (temp.NextElement is null)
                {
                    throw new ArgumentException($"No argument with value {value}");
                }
                temp = temp.NextElement!;
            }
            return temp;

        }

        // изменить значение ноды
        public virtual void SetValue(T element, int nodeNumber)
        {
            GetNodeByNumber(nodeNumber - 1).NextElement.Field = element;
        }

        public virtual bool Remove(T value)
        {
            try
            {
                Node<T> tempNode = GetNodeByNumber(IndexOf(value) - 1);

                if(tempNode.NextElement.NextElement is null)
                {
                    tempNode.NextElement = null;                   
                }
                else
                {
                    tempNode.NextElement = tempNode.NextElement.NextElement;
                }
                nodesCount--;
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public virtual void RemoveAt(int nodeNumber)
        {
            if (nodeNumber == nodesCount - 1) // елси элемент последний
            {
                Node<T> temp = GetNodeByNumber(nodeNumber - 1);
                temp.NextElement = null;
                nodesCount--;
            }
            else if (IsNodeExist(nodeNumber)) // если элемент есть в списке
            {
                if (nodeNumber == 0) // замена начала
                {
                    _begin = _begin.NextElement;
                    nodesCount--;
                    return;
                }

                Node<T> temp = GetNodeByNumber(nodeNumber - 1);
                temp.NextElement = temp.NextElement.NextElement;
                nodesCount--;
            }
        }

        public static void ShowLinkedList(NewLinkedList<T> list) // Вывод всех нод листа
        {
            if (list._begin == null)
            {
                return;
            }

            Console.WriteLine("This List has {0} elements", list.nodesCount);

            Node<T> temp = list._begin;
            for (int node = 0; node < list.nodesCount; node++)
            {
                if (temp == null)
                {
                    continue;
                }
                Console.WriteLine("Node: {0}, with field: {1}", node, temp.Field);
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

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            foreach (T item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }            
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new NewLinkedListEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => new NewLinkedListEnumerator<T>(this);
    }    
}