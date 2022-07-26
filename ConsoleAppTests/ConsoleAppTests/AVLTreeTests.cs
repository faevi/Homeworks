using ConsoleApp;
namespace ConsoleAppTests
{
    [TestClass]
    public class AVLTreeTests
    {
        
        [TestMethod]
        public void InsertInAVLTree_String_phone()
        {
            AVLTree tree = new AVLTree();
            tree.Insert("phone",10);
            Assert.AreEqual("phone", tree.Find(10).Value);
        }


        [TestMethod]
            public void InsertInAVLTreeLeftRotate()
        {
            AVLTree tree = new AVLTree();
            tree.Insert("phone", 10);
            tree.Insert("cat", 20);
            tree.Insert("dog", 30);
            tree.Insert("hand", 40);
            tree.Insert("leg", 50);
            tree.PrintInfix();
            Assert.AreEqual("phone", tree.Find(10).Value);
            Assert.AreEqual("cat", tree.Find(20).Value);
            Assert.AreEqual("dog", tree.Find(30).Value);
            Assert.AreEqual("hand", tree.Find(40).Value);
            Assert.AreEqual("leg", tree.Find(50).Value);
        }

        [TestMethod]
        public void InsertInAVLTreeBigLeftRotate()
        {
            AVLTree tree = new AVLTree();
            tree.Insert("phone", 10);
            tree.Insert("cat", 20);
            tree.Insert("dog", 15);
            Assert.AreEqual("phone", tree.Find(10).Value);
            Assert.AreEqual("cat", tree.Find(20).Value);
            Assert.AreEqual("dog", tree.Find(15).Value);
        }

        [TestMethod]
        public void InsertInAVLTreeBigRightRotate()
        {
            AVLTree tree = new AVLTree();
            tree.Insert("phone", 10);
            tree.Insert("cat", 5);
            tree.Insert("dog", 7);
            tree.PrintInfix();
            Assert.AreEqual("phone", tree.Find(10).Value);
            Assert.AreEqual("cat", tree.Find(5).Value);
            Assert.AreEqual("dog", tree.Find(7).Value);
        }

        [TestMethod]
        public void InsertInAVLTree_Strings()
        {
            AVLTree tree = new AVLTree();
            tree.Insert("phone", 10);
            tree.Insert("fish", 15);
            tree.Insert("dog", 20);
            tree.Insert("cat", 25);
            tree.Insert("civilization", 24);
            tree.Insert("leg", 100);
            tree.Insert("hand", 49);
            tree.Insert("jaws", 74);
            tree.Insert("table", 28);
            tree.Insert("silence", 395);
            tree.PrintInfix();
            Assert.AreEqual("phone", tree.Find( 10).Value);
            Assert.AreEqual("fish", tree.Find( 15).Value);
            Assert.AreEqual("dog", tree.Find( 20).Value);
            Assert.AreEqual("cat", tree.Find( 25).Value);
            Assert.AreEqual("civilization", tree.Find(24).Value);
        }

        [TestMethod]
        public void RemoveInAVLTree_String_phone_fish_dog_cat_civilization()
        {
            AVLTree tree = new AVLTree();
            tree.Insert("phone",10);
            tree.Insert("fish",30);
            tree.Insert("dog",40);
            tree.Insert("cat",50);
            tree.Insert("civilization",55);
            tree.PrintInfix();
            Console.WriteLine($"Dog value {tree.Find(40).Value}");
            Assert.IsTrue(tree.Remove(55));
            Assert.IsTrue(tree.Remove(40));
            Assert.IsTrue(tree.Remove(10));
            Console.WriteLine("all removed");
            tree.PrintInfix();
        }

        [TestMethod]
        public void InsertInAVLTree_BigRemove()
        {
            AVLTree tree = new AVLTree();
            tree.Insert("phone", 10);
            tree.Insert("fish", 15);
            tree.Insert("dog", 20);
            tree.Insert("cat", 25);
            tree.Insert("civilization", 3);
            tree.Insert("leg", 100);
            tree.PrintInfix();
            tree.Insert("hand", 49);
            tree.Insert("jaws", 74);
            tree.Insert("table", 28);
            tree.Insert("silence", 395);
            
            Console.WriteLine("Lets start");
            Assert.IsTrue(tree.Remove(395));
            tree.PrintInfix();
            Assert.IsTrue(tree.Remove(74));
            tree.PrintInfix();
            Assert.IsTrue(tree.Remove(20));
            tree.PrintInfix();
            Assert.IsTrue(tree.Remove(25));
            Assert.IsTrue(tree.Remove(49));
            
        }
    }
}