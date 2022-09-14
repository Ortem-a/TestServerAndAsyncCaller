using Solution;

namespace Tests
{
    [TestClass]
    public class MyServerTest
    {
        [TestMethod]
        public void AddToCountTest()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tasks.Add(
                        Task.Factory.StartNew((() => MyServer.GetCount())));
                }
                tasks.Add(
                    Task.Factory.StartNew(() => MyServer.AddToCount(1)));
            }

            Task.WaitAll(tasks.ToArray());

            Assert.AreEqual(1000, MyServer.GetCount());
        }
    }
}