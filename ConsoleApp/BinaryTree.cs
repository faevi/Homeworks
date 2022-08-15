using System.Collections;

namespace ConsoleApp
{
    public class BinaryTree<T>
    {
        protected class Node<T>
        {
            private T _value;
            private int _key;
            public T Value
            {
                get => _value;
                set
                {
                    _value = value;
                }
            }
            public int Key
            {
                get => _key;
                set
                {
                    _key = value;
                }
            }
            public Node<T>? LeftElement { get; set; }
            public Node<T>? RightElement { get; set; }

            public Node()
            {

            }

            public Node(T value, int key)
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Value = value;
                Key = key;
            }

            public Node(Node<T> node)
            {
                Value = node.Value;
                Key = node.Key;
                LeftElement = node.LeftElement;
                RightElement = node.RightElement;
            }
        }

        protected Node<T>? _rootNode;
        protected int _nodesCount = 0;

        public bool IsReadOnly => false;

        private BinaryTree(Node<T>? rootNode)
        {
            _rootNode = rootNode;
        }

        public BinaryTree()
        {
            _rootNode=null;
        }

        public T Find(int key) => FindNode(key).Value;

        private Node<T> FindNode(int key)
        {
            Node<T> tempNode = _rootNode;

            while (tempNode.Key != key)
            {
                if (tempNode == null)
                {
                    throw new KeyNotFoundException($"No element in tree with {key} key");
                }
                else if (tempNode.Key == key)
                {
                    return tempNode;
                }

                if (key > tempNode.Key)
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

        public virtual void Insert(T value, int key)
        {
            if (_rootNode is null)
            {
                _rootNode = new Node<T>(value, key);
                return;
            }

            Node<T> tempNode = _rootNode;

            while (tempNode.Key != key)
            {
                if (key > tempNode.Key)
                {
                    if (tempNode.RightElement == null)
                    {
                        tempNode.RightElement = new Node<T>(value, key);
                        _nodesCount++;
                        return;
                    }

                    tempNode = tempNode.RightElement!;
                }
                else if (key < tempNode.Key)
                {
                    if (tempNode.LeftElement == null)
                    {
                        tempNode.LeftElement = new Node<T>(value, key);
                        _nodesCount++;
                        return;
                    }

                    tempNode = tempNode.LeftElement!;
                }
                else
                {
                    tempNode.Value = value;
                    return;
                }
            }
        }

        public virtual bool TryRemove(int key)
        {
            Node<T> tempNode = _rootNode;
            Node<T> prevTempNode = new Node<T>();
            bool isPrevNodeLeft = false;

            while (tempNode.Key != key)
            {
                prevTempNode = tempNode;

                if (key > tempNode.Key)
                {
                    isPrevNodeLeft = false;
                    if (tempNode.RightElement == null)
                    {
                        return false;
                    }

                    tempNode = tempNode.RightElement;
                }
                else if (key < tempNode.Key)
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
                    tempNode.Key = tempNode.RightElement.Key;
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

                    int tempKey = secondTempNode.Key;
                    tempNode.Value = secondTempNode.Value;
                    TryRemove(tempKey);
                    tempNode.Key = tempKey;
                    _nodesCount--;
                    return true;
                }
            }
        }

        public virtual void PrintInfix()
        {
            if (this._rootNode is null)
            {
                return;
            }

            BinaryTree<T> subLeftTree = new BinaryTree<T>(this._rootNode.LeftElement);
            subLeftTree.PrintInfix();
            Console.WriteLine($"{this._rootNode.Value} in tree");
            BinaryTree<T> subRightTree = new BinaryTree<T>(this._rootNode.RightElement);
            subRightTree.PrintInfix();
        }

        public virtual void PrintPostfix()
        {
            if (this._rootNode is null)
            {
                return;
            }

            BinaryTree<T> subRightTree = new BinaryTree<T>(this._rootNode.RightElement);
            subRightTree.PrintPostfix();
            Console.WriteLine($"{this._rootNode.Value} in tree");
            BinaryTree<T> subLeftTree = new BinaryTree<T>(this._rootNode.LeftElement);
            subLeftTree.PrintPostfix();
        }

        public bool Add(T item)
        {
            Insert(item, item.GetHashCode());
            return true;
        }
    }
}
