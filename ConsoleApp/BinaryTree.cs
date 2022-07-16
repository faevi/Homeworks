namespace ConsoleApp
{
    public class BinaryTree<T>
    {
        public class Node<T>
        {
            private T _value;
            public T Value
            {
                get => _value;
                set
                {
                    _value = value;
                    key = value!.GetHashCode();
                }
            }

            public int key;
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

                Value = value!;
            }
        }

        public Node<T>? _parrentNode;

        public BinaryTree(Node<T>? parrentNode)
        {
            _parrentNode =  parrentNode;          
        }

        public BinaryTree()
        {
            _parrentNode=null;
        }        

        public ref Node<T> Find(T value)
        {
            int valueKey = value.GetHashCode();

            if (this._parrentNode == null)
            {
                throw new KeyNotFoundException($"No element in tree with {value} value");
            } 
            else if (this._parrentNode.key == valueKey)
            {
                return ref this._parrentNode!;
            }

            BinaryTree<T> subBinaryTree = new BinaryTree<T>();
            
            if (valueKey > this._parrentNode.key)
            {
                subBinaryTree._parrentNode = this._parrentNode.RightElement;
            } 
            else
            {
                subBinaryTree._parrentNode = this._parrentNode.LeftElement;
            }
            return ref subBinaryTree.Find(value);
        }

        public void Insert(T value)
        {    
            if (_parrentNode == null)
            {
                _parrentNode = new Node<T>(value);
                return;
            }

            int valueKey = value.GetHashCode();
            BinaryTree<T> tempTree;

            if (valueKey > _parrentNode.key)
            {
                tempTree = new BinaryTree<T>(_parrentNode.RightElement);
                _parrentNode.RightElement = tempTree._parrentNode;
                if (tempTree._parrentNode == null)
                {
                    _parrentNode.RightElement = new Node<T>(value);
                    return ;
                }
            }
            else if (valueKey < _parrentNode.key)
            {
                tempTree = new BinaryTree<T>(_parrentNode.LeftElement);
                _parrentNode.LeftElement = tempTree._parrentNode;
                if (tempTree._parrentNode == null)
                {
                    _parrentNode.LeftElement = new Node<T>(value);
                    return;
                }
            }
            else
            {
                _parrentNode.Value = value;
                return;
            }
            
            tempTree.Insert(value);
        }

        public bool Remove(T value)
        {
            if (this._parrentNode == null)
            {
                return false;
            }

            bool isRemoved;
            int valueKey = value.GetHashCode();
            BinaryTree<T> tempTree;

            if (valueKey > this._parrentNode.key)
            {
                tempTree = new BinaryTree<T>(_parrentNode.RightElement);
                
                if (tempTree._parrentNode == null)
                {
                    return false;
                }

                isRemoved = tempTree.Remove(value);
                _parrentNode.RightElement = tempTree._parrentNode;
                return isRemoved;
            }
            else if (valueKey < this._parrentNode.key)
            {
                tempTree = new BinaryTree<T>(_parrentNode.LeftElement);
                
                if (tempTree._parrentNode == null)
                {
                    return false;
                }

                isRemoved = tempTree.Remove(value);
                _parrentNode.LeftElement = tempTree._parrentNode;
                return isRemoved;
            }
            else
            {
                if (_parrentNode.LeftElement is null && _parrentNode.RightElement is null)
                {
                    _parrentNode = null;
                    return true;
                } 
                else if (_parrentNode.LeftElement is null)
                {                    
                    _parrentNode = _parrentNode.RightElement;
                    return true;
                }
                else if (_parrentNode.RightElement is null)
                {
                    _parrentNode = _parrentNode.LeftElement;
                    return true;
                }
                else
                {
                    if (_parrentNode.RightElement.LeftElement is null)
                    {
                        _parrentNode.Value =_parrentNode.RightElement.Value;
                        _parrentNode.RightElement = _parrentNode.RightElement.RightElement;
                        return true;
                    }
                    else
                    {
                        tempTree = new BinaryTree<T>(_parrentNode.RightElement);

                        while (tempTree._parrentNode.LeftElement is not null)
                        {
                            tempTree._parrentNode = tempTree._parrentNode.LeftElement;
                        }

                        this._parrentNode.Value = tempTree._parrentNode.Value;
                        return tempTree.Remove(tempTree._parrentNode.Value);                        
                    }
                }
            }
        }

        public void PrintInfix()
        {
            if (this._parrentNode is null)
            {
                return;
            }

            BinaryTree<T> subLeftTree = new BinaryTree<T>(this._parrentNode.LeftElement);
            subLeftTree.PrintInfix();
            Console.WriteLine($"{this._parrentNode.Value} int tree");
            BinaryTree<T> subRightTree = new BinaryTree<T>(this._parrentNode.RightElement);
            subRightTree.PrintInfix();
        }

        public void PrintPostfix()
        {
            if (this._parrentNode is null)
            {
                return;
            }

            BinaryTree<T> subRightTree = new BinaryTree<T>(this._parrentNode.RightElement);
            subRightTree.PrintPostfix();
            Console.WriteLine($"{this._parrentNode.Value} int tree");
            BinaryTree<T> subLeftTree = new BinaryTree<T>(this._parrentNode.LeftElement);
            subLeftTree.PrintPostfix(); 
        }
    }
}
