using ConsoleApp;
namespace ConsoleAppTests
{
    [TestClass]
    public class NewSetTests
    {
        [TestMethod]
        public void NewSetMethodTests()
        {
            NewSet<int> newSet = new NewSet<int>();
            newSet.Add(1);
            newSet.Add(2);
            newSet.Add(100);
            newSet.Add(9);
            Assert.IsTrue(newSet.Remove(100));
            Assert.IsTrue(newSet.Contains(9));
            Console.WriteLine(newSet.Count);
            Assert.IsTrue(newSet.Count == 3);
        }
    }
}
