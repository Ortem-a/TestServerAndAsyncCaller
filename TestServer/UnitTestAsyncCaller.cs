using Solution;

namespace Tests
{
    [TestClass]
    public class AsyncCallerTestClass
    {
        [TestMethod]
        public void AsyncCallerTestMethod()
        {
            EventHandler handler = new EventHandler((sender, args) => Thread.Sleep(300));
            AsyncCaller asyncCaller = new AsyncCaller(handler);

            var good = asyncCaller.Invoke(400, null, EventArgs.Empty);

            Assert.IsTrue(good);

            Assert.IsFalse(asyncCaller.Invoke(200, null, EventArgs.Empty));
        }
    }
}