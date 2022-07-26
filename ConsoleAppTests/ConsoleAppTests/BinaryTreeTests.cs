using ConsoleApp;
namespace ConsoleAppTests
{
    [TestClass]
    public class BinaryTreeTests
    {
        
        [TestMethod]
        public void InsertInBinaryTree5()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(5, 5);
            Assert.AreEqual(5, tree.Find(5, 5).Value);
        }       

        [TestMethod]
        public void FindInBinaryTree_5_10_100_1_3()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(5, 5);
            tree.Insert(10,10);
            tree.Insert(100, 100);
            tree.Insert(1, 1);
            tree.Insert(3, 3);
            Assert.AreEqual(5, tree.Find(5, 5).Value);
            Assert.AreEqual(10, tree.Find(10, 10).Value);
            Assert.AreEqual(100, tree.Find(100, 100).Value);
            Assert.AreEqual(1, tree.Find(1, 1).Value);
            Assert.AreEqual(3, tree.Find(3, 3).Value);
        }
        
        [TestMethod]
        public void RemoveInBinaryTreeFirstTest()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(100, 100);
            tree.Insert(4, 4);
            tree.Insert(10, 10);
            tree.Insert(8, 8);
            tree.Insert(3, 3);
            tree.Insert(15, 15);
            tree.Insert(2, 2);
            tree.Insert(192, 192);
            tree.Insert(592, 592);
            tree.Insert(92, 92);
            Assert.IsTrue(!tree.Remove(5));
            Assert.IsTrue(tree.Remove(10));
            Assert.IsTrue(tree.Remove(100));
            Assert.IsTrue(!tree.Remove(1));
            Assert.IsTrue(tree.Remove(3));
            Assert.IsTrue(tree.Remove(192));
            Assert.IsTrue(tree.Remove(92));
            tree.PrintInfix();
        }
        
        [TestMethod]
        public void RemoveInBinaryTreeSecondTest()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(100, 100);
            tree.Insert(200, 200);
            tree.Insert(300, 300);
            tree.Insert(150, 150);
            tree.Insert(301, 301);
            tree.Insert(299, 299);
            tree.Insert(298, 298);
            tree.Insert(297, 297);
            tree.Insert(296, 296);
            tree.Insert(295, 295);
            Assert.IsTrue(tree.Remove(300));
            Assert.IsTrue(tree.Remove(295));
            Assert.IsTrue(tree.Remove(296));
            Assert.IsTrue(tree.Remove(297));
            tree.PrintInfix();
        }
    }
}