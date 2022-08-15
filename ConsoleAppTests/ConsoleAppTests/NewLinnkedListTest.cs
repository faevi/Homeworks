using ConsoleApp;
namespace ConsoleAppTests
{
    [TestClass]
    public class NewLinnkedListTest
    {
        [TestMethod]
        public void ListMethodTests()
        {
            NewLinkedList<int> list = new NewLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.IsTrue(list.Contains(5));
            Assert.IsTrue(list.Remove(4));
            Assert.IsFalse(list.Contains(4));
        }
    }
}
