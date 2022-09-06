using System.Collections;

namespace ConsoleApp
{
    public class NewSet<T> : ISet<T> where T : IComparable<T>
    {
        private class Node<T>
        {
            private T _value;
            public T Value
            {
                get => _value;
                set
                {
                    _value = value;
                }
            }

            public Node<T>? LeftElement { get; set; }
            public Node<T>? RightElement { get; set; }

            public Node()
            {

            }

            public Node(T value)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Value = value;
            }

            public Node(Node<T> node)
            {
                Value = node.Value;
                LeftElement = node.LeftElement;
                RightElement = node.RightElement;
            }
        }

        private Node<T> _rootNode;
        private int _nodesCount = 0;

        public int Count => _nodesCount;

        public bool IsReadOnly => false;

        public bool Add(T value)
        {
            if (_rootNode is null)
            {
                _rootNode = new Node<T>(value);
                _nodesCount++;
                return true;
            }

            Node<T> tempNode = _rootNode;

            while (!tempNode.Value.Equals(value))
            {
                if (value.CompareTo(tempNode.Value) > 0)
                {
                    if (tempNode.RightElement == null)
                    {
                        tempNode.RightElement = new Node<T>(value);
                        _nodesCount++;
                        return true;
                    }

                    tempNode = tempNode.RightElement!;
                }
                else
                {
                    if (tempNode.LeftElement == null)
                    {
                        tempNode.LeftElement = new Node<T>(value);
                        _nodesCount++;
                        return true;
                    }

                    tempNode = tempNode.LeftElement!;
                }                
            }

            tempNode.Value = value;
            return true;
        }

        void ISet<T>.ExceptWith(IEnumerable<T> other)
        {
            foreach (T element in other)
            {
                Remove(element);
            }
        }

        void ISet<T>.IntersectWith(IEnumerable<T> other)
        {
            Clear();

            foreach (T element in other)
            {
                Add(element);
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            foreach(T element in this)
            {
                if (!other.Contains(element))
                {
                    return false;
                }               
            }

            return _nodesCount < other.Count();
        }

        bool ISet<T>.IsProperSupersetOf(IEnumerable<T> other)
        {
            try
            {
                foreach (T element in other)
                {
                    FindNode(element);
                }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return _nodesCount > other.Count();
        }

        bool ISet<T>.IsSubsetOf(IEnumerable<T> other)
        {
            foreach (T element in this)
            {
                if (!other.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        bool ISet<T>.IsSupersetOf(IEnumerable<T> other)
        {
            try
            {
                foreach (T element in other)
                {
                    FindNode(element);
                }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }

        bool ISet<T>.Overlaps(IEnumerable<T> other)
        {
            foreach (T element in other)
            {
                try
                {
                    FindNode(element);
                    return true;
                }
                catch (KeyNotFoundException)
                {

                }
            }
            return false;
        }

        bool ISet<T>.SetEquals(IEnumerable<T> other)
        {
            if (other.Count() != _nodesCount)
            {
                return false;
            }

            foreach (T element in other)
            {
                try
                {
                    FindNode(element);
                }
                catch (KeyNotFoundException)
                {
                    return false;
                }
            }

            return true;
        }

        void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
        {
            foreach (T element in other)
            {
                try
                {
                    FindNode(element);
                    Remove(element);
                }
                catch (KeyNotFoundException)
                {

                }
            }
        }

        void ISet<T>.UnionWith(IEnumerable<T> other)
        {
            foreach (T element in other)
            {
                Add(element);
            }
        }

        void ICollection<T>.Add(T value)
        {
            if (_rootNode is null)
            {
                _rootNode = new Node<T>(value);
                return;
            }

            Node<T> tempNode = _rootNode;

            while (!tempNode.Value.Equals(value))
            {
                if (value.CompareTo(tempNode.Value) > 0)
                {
                    if (tempNode.RightElement == null)
                    {
                        tempNode.RightElement = new Node<T>(value);
                        _nodesCount++;
                        return;
                    }

                    tempNode = tempNode.RightElement!;
                }
                else
                {
                    if (tempNode.LeftElement == null)
                    {
                        tempNode.LeftElement = new Node<T>(value);
                        _nodesCount++;
                        return;
                    }

                    tempNode = tempNode.LeftElement!;
                }
            }

            tempNode.Value = value;
        }

        public void Clear()
        {
            _nodesCount = 0;
            _rootNode = null;
        }

        bool ICollection<T>.Contains(T value)
        {
            try
            {
                return FindNode(value).Value.Equals(value);
            }
            catch (KeyNotFoundException)
            {
                return false;
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

        public bool Remove(T value)
        {
            Node<T> tempNode = _rootNode;
            Node<T> prevTempNode = new Node<T>();
            bool isPrevNodeLeft = false;

            while (tempNode.Value.Equals(value))
            {
                prevTempNode = tempNode;

                if (value.CompareTo(tempNode.Value) > 0)
                {
                    isPrevNodeLeft = false;
                    if (tempNode.RightElement == null)
                    {
                        return false;
                    }

                    tempNode = tempNode.RightElement;
                }
                else
                {
                    isPrevNodeLeft = true;

                    if (tempNode.LeftElement == null)
                    {
                        return false;
                    }

                    tempNode = tempNode.LeftElement;
                }
            }

            if (tempNode.LeftElement is null && tempNode.RightElement is null)
            {
                tempNode = null;

                if (isPrevNodeLeft)
                {
                    prevTempNode.LeftElement = null;
                }
                else
                {
                    prevTempNode.RightElement = null;
                }

                _nodesCount--;
                return true;
            }
            else if (tempNode.LeftElement is null)
            {
                tempNode = new Node<T>(tempNode.RightElement);
                _nodesCount--;
                return true;
            }
            else if (tempNode.RightElement is null)
            {
                tempNode = new Node<T>(tempNode.LeftElement);
                _nodesCount--;
                return true;
            }
            else
            {
                if (tempNode.RightElement.LeftElement is null)
                {
                    tempNode.Value = tempNode.RightElement.Value;
                    tempNode.RightElement = tempNode.RightElement.RightElement;
                    _nodesCount--;
                    return true;
                }
                else
                {
                    Node<T> secondTempNode = tempNode.RightElement;

                    while (secondTempNode.LeftElement is not null)
                    {
                        secondTempNode = secondTempNode.LeftElement;
                    }

                    T tempValue = secondTempNode.Value;
                    tempNode.Value = secondTempNode.Value;
                    Remove(tempValue);
                    _nodesCount--;
                    return true;
                }
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new NewSetEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => new NewSetEnumerator<T>(this);

        private Node<T> FindNode(T value)
        {
            Node<T> tempNode = _rootNode;

            while (!tempNode.Value.Equals(value))
            {
                if (tempNode == null)
                {
                    throw new KeyNotFoundException($"No element in tree with {value} value");
                }

                if (value.CompareTo(tempNode.Value) > 0)
                {
                    tempNode = tempNode.RightElement;
                }
                else
                {
                    tempNode = tempNode.LeftElement;
                }

            }

            return tempNode;
        }

        private class NewSetEnumerator<T> : IEnumerator<T> where T : IComparable<T>
        {
            private bool disposedValue;
            private NewSet<T>.Node<T> _current;
            private NewSet<T> _set;
            private Stack<NewSet<T>.Node<T>> _stack;
            private int _index = -1;

            public NewSetEnumerator(NewSet<T> set)
            {
                _set = set;
                _current = null;
            }

            T IEnumerator<T>.Current => _current.Value;

            object IEnumerator.Current => _current.Value;

            public bool MoveNext()
            {
                if (_index == -1)
                {
                    _current = _set._rootNode;
                    _index++;
                    _stack.Push(_current);
                    return true;
                }

                if(_current.LeftElement is not null)
                {
                    _stack.Push(_current.LeftElement);
                    _current = _current.LeftElement;
                    _index++;
                    return true;
                }
                else
                {
                    while (_current.RightElement is null)
                    {
                        _current = _stack.Pop();
                        if (_stack.Count == 0)
                        {
                            return false;
                        }
                    }

                    _stack.Push(_current.RightElement);
                    _current = _current.RightElement;
                    _index++;
                    return true;
                }                
            }

            void IEnumerator.Reset()
            {
                _current = null;
                _index = -1;
                _stack.Clear();
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    disposedValue=true;
                }
            }

            void IDisposable.Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
