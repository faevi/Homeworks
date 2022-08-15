using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class AVLTree : BinaryTree<string>
    {
        
        protected class AVLNode : Node<string>
        {
            internal int heightDiff = 0;
            public new AVLNode? LeftElement { get; set; }
            public new AVLNode? RightElement { get; set; }
            public AVLNode()
            {

            }
            public AVLNode(string value, int key) : base(value, key)
            {

            }
            public AVLNode(AVLNode node)
            {
                Value = node.Value;
                Key = node.Key;
                heightDiff = node.heightDiff;

                if (node.LeftElement is not null)
                {
                    LeftElement = node.LeftElement;
                }

                if (node.RightElement is not null)
                {
                    RightElement = node.RightElement;
                }
            }
        }

        private AVLNode? _rootNode;

        public AVLTree()
        {

        }
        private AVLTree(AVLNode? rootNode)
        {
            _rootNode = rootNode;
        }       

        private AVLNode Find(int key)
        {
            AVLNode tempNode = _rootNode;

            while (tempNode.Key != key)
            {
                if (tempNode == null)
                {
                    throw new KeyNotFoundException($"No element in tree with key:{key}");
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

        public override void Insert(string value, int key)
        {
            if (_rootNode == null)
            {
                _rootNode = new AVLNode(value, key);
                return;
            }

            List<int> reblanceKeys = new List<int>();
            AVLNode tempNode = _rootNode;

            while (tempNode.Key != key)
            {
                if (key > tempNode.Key)
                {
                    tempNode.heightDiff--;

                    if(Math.Abs(tempNode.heightDiff) > 1)
                    {
                        reblanceKeys.Add(tempNode.Key);
                    }

                    if (tempNode.RightElement == null)
                    {
                        tempNode.RightElement = new AVLNode(value, key);
                        Rebalance(reblanceKeys);
                        return;
                    }

                    tempNode = tempNode.RightElement;                    
                }
                else if (key < tempNode.Key)
                {
                    tempNode.heightDiff++;

                    if (Math.Abs(tempNode.heightDiff) > 1)
                    {
                        reblanceKeys.Add(tempNode.Key);
                    }

                    if (tempNode.LeftElement == null)
                    {
                        tempNode.LeftElement = new AVLNode(value, key);
                        Rebalance(reblanceKeys);
                        return;
                    }

                    tempNode = tempNode.LeftElement;
                }
                else
                {
                    tempNode.Value = value;
                    Rebalance(reblanceKeys);
                    return;
                }
            }
        }   
        

        public override bool TryRemove(int key)
        {
            if (_rootNode == null)
            {
                return false;
            }

            bool isPrevNodeLeft = false;
            List<int> reblanceKeys = new List<int>();
            AVLNode tempNode = _rootNode;
            AVLNode prevTempNode = new AVLNode();

            while (key != tempNode.Key)
            {
                prevTempNode = tempNode;

                if (key > tempNode.Key)
                {
                    isPrevNodeLeft = false;
                    tempNode.heightDiff++;

                    if (Math.Abs(tempNode.heightDiff) > 1)
                    {
                        reblanceKeys.Add(tempNode.Key);
                    }

                    if (tempNode.RightElement == null)
                    {
                        return false;
                    }

                    tempNode = tempNode.RightElement;
                }
                else if (key < tempNode.Key)
                {
                    isPrevNodeLeft = true;
                    tempNode.heightDiff--;

                    if (Math.Abs(tempNode.heightDiff) > 1)
                    {
                        reblanceKeys.Add(tempNode.Key);
                    }

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

                return true;
            }
            else if (tempNode.LeftElement is null)
            {
                tempNode = new AVLNode(tempNode.RightElement);

                if (isPrevNodeLeft)
                {
                    prevTempNode.LeftElement = tempNode;
                }
                else
                {
                    prevTempNode.RightElement = tempNode;
                }                
                
                return true;
            }
            else if (tempNode.RightElement is null)
            {
                tempNode = new AVLNode(tempNode.LeftElement);

                if (isPrevNodeLeft)
                {
                    prevTempNode.LeftElement = tempNode;
                }
                else
                {
                    prevTempNode.RightElement = tempNode;
                }                
                
                return true;
            }
            else
            {
                if (tempNode.RightElement.LeftElement is null)
                {
                    tempNode.Value = tempNode.RightElement.Value;
                    tempNode.Key = tempNode.RightElement.Key;
                    tempNode.heightDiff = tempNode.RightElement.heightDiff + 1;
                    tempNode.RightElement = tempNode.RightElement.RightElement;
                    return true;
                }
                else
                {
                    AVLNode secondTempNode = tempNode.RightElement;

                    while (secondTempNode.LeftElement is not null)
                    {
                        secondTempNode = secondTempNode.LeftElement;
                    }

                    int tempKey = secondTempNode.Key;
                    tempNode.Value = secondTempNode.Value;
                    TryRemove(tempKey);
                    tempNode.Key = tempKey;
                    return true;
                }
            }
        }

        public override void PrintInfix()
        {
            if (this._rootNode is null)
            {
                return;
            }

            AVLTree subLeftTree = new AVLTree(_rootNode.LeftElement);
            subLeftTree.PrintInfix();
            Console.WriteLine($"{_rootNode.Value} in tree");
            AVLTree subRightTree = new AVLTree(_rootNode.RightElement);
            subRightTree.PrintInfix();
        }

        private void Rebalance(List<int> rebalanceKeys)
        {
            rebalanceKeys.Reverse();

            foreach (int key in rebalanceKeys)
            {
                bool isRootNode = _rootNode.Key == key;
                AVLNode tempNode = Find(key);
                
                switch (tempNode.heightDiff)
                {
                    case -2:
                        switch (tempNode.RightElement.heightDiff)
                        {
                            case -1:
                                Equate(tempNode , RotateLeft(tempNode));

                                if (isRootNode)
                                {
                                    _rootNode = tempNode;
                                }

                                break;
                            case 0:
                                Equate(tempNode, RotateLeft(tempNode));

                                if (isRootNode)
                                {
                                    _rootNode = tempNode;
                                }

                                break;
                            case 1:
                                Equate(tempNode, BigRotateLeft(tempNode));

                                if (isRootNode)
                                {
                                    _rootNode = tempNode;
                                }

                                break;
                        }
                        break;
                    case 2:
                        switch (tempNode.LeftElement.heightDiff)
                        {
                            case 1:
                                Equate(tempNode , RotateRight(tempNode));

                                if (isRootNode)
                                {
                                    _rootNode = tempNode;
                                }

                                break;
                            case 0:
                                Equate(tempNode, RotateRight(tempNode));

                                if (isRootNode)
                                {
                                    _rootNode = tempNode;
                                }

                                break;
                            case -1:
                                Equate(tempNode, BigRotateRight(tempNode));

                                if (isRootNode)
                                {
                                    _rootNode = tempNode;
                                }

                                break;
                        }
                        break;
                }
            }

            rebalanceKeys.Clear();
        }

        private AVLNode RotateLeft(AVLNode root)
        {
            AVLNode RightNode = new AVLNode(root.RightElement);

            if(RightNode.LeftElement is not null)
            {
                root.RightElement =  new AVLNode(RightNode.LeftElement);
            }
            else
            {
                root.RightElement = null;
            }

            if (root.heightDiff == -1)
            {
                root.heightDiff = RightNode.heightDiff == -1 ? 1 : 0;
            }
            else
            {
                root.heightDiff = RightNode.heightDiff == -1 ? 0 : -1;
            }
            
            RightNode.LeftElement = new AVLNode(root);
            RightNode.heightDiff = RightNode.heightDiff == -1 ? 0 : 1;
            return new AVLNode(RightNode);
        }

        private AVLNode RotateRight(AVLNode root)
        {
            AVLNode LeftNode = new AVLNode(root.LeftElement);

            if(LeftNode.RightElement is not null)
            {
                root.LeftElement =  LeftNode.RightElement;
            } else
            {
                root.LeftElement = null;
            } 
            
            if (root.heightDiff == 1)
            {
                root.heightDiff = LeftNode.heightDiff == 1 ? -1 : 0;
            } else
            {
                root.heightDiff = LeftNode.heightDiff == 1 ? 0 : 1;
            }
            
            LeftNode.RightElement =  new AVLNode(root);
            LeftNode.heightDiff = LeftNode.heightDiff == 1 ? 0 : -1;
            return new AVLNode(LeftNode);
        }

        private AVLNode BigRotateLeft(AVLNode root)
        {
            root.RightElement = new AVLNode(RotateRight(root.RightElement));
            return new AVLNode(RotateLeft(root));
        }

        private AVLNode BigRotateRight(AVLNode root)
        {
            root.LeftElement = new AVLNode(RotateLeft(root.LeftElement));
            return new AVLNode(RotateRight(root));
        }
        
        private void Equate(AVLNode firstNode, AVLNode secondNode)
        {
            firstNode.heightDiff = secondNode.heightDiff;
            firstNode.Key = secondNode.Key;
            firstNode.Value = secondNode.Value;
            firstNode.LeftElement = secondNode.LeftElement;
            firstNode.RightElement = secondNode.RightElement; 
        }
    }
}
