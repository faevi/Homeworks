using ConsoleApp;
namespace ConsoleAppTests
{
    [TestClass]
    public class BinaryTree
    {
        [TestMethod]
        public void InsertInBinaryTree5()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(5);
            Assert.AreEqual(5, tree.Find(5).Value);
        }

        [TestMethod]
        public void InsertInBinaryTree_5_10_100_10000_10()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(5);
            tree.Insert(10);
            tree.Insert(100);
            tree.Insert(10000);
            tree.Insert(10);
            Assert.AreEqual(5, tree._parrentNode.Value);
            Assert.AreEqual(10, tree._parrentNode.RightElement.Value);
            Assert.AreEqual(100, tree._parrentNode.RightElement.RightElement.Value);
            Assert.AreEqual(10000, tree._parrentNode.RightElement.RightElement.RightElement.Value);
        }

        [TestMethod]
        public void FindInBinaryTree_5_10_100_1_3()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(5);
            tree.Insert(10);
            tree.Insert(100);
            tree.Insert(1);
            tree.Insert(3);
            Assert.AreEqual(5, tree.Find(5).Value);
            Assert.AreEqual(10, tree.Find(10).Value);
            Assert.AreEqual(100, tree.Find(100).Value);
            Assert.AreEqual(1, tree.Find(1).Value);
            Assert.AreEqual(3, tree.Find(3).Value);
        }

        [TestMethod]
        public void RemoveInBinaryTree_5_10_100_1_3()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(100);
            tree.Insert(4);
            tree.Insert(10);
            tree.Insert(8);
            tree.Insert(3);
            tree.Insert(15);
            tree.Insert(2);
            tree.Insert(192);
            tree.Insert(592);
            tree.Insert(92);
            Assert.IsTrue(!tree.Remove(5));
            Assert.IsTrue(tree.Remove(10));
            Assert.IsTrue(tree.Remove(100));
            Assert.IsTrue(!tree.Remove(1));
            Assert.IsTrue(tree.Remove(3));
            Assert.IsTrue(tree.Remove(192));
            Assert.IsTrue(tree.Remove(92));
        }
    }
}