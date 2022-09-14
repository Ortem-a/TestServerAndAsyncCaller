namespace Solution
{
    public static class MyServer
    {
        private static int _count = 0; // инициализация счетчика
        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public static void StartMyServer()
        {
            Parallel.For(0, 50, i =>
            {
                if (i % 2 == 1)
                {
                    MyServer.AddToCount(1);
                    Console.WriteLine($"Time: {DateTime.Now.Millisecond} ms. AddToCount");
                }
                else
                {
                    Console.WriteLine($"Time: {DateTime.Now.Millisecond} ms. {MyServer.GetCount()}");
                }
            });
            Console.WriteLine(MyServer.GetCount());
            Console.WriteLine("STOP");
        }

        public static int GetCount()
        {
            _lock.EnterReadLock(); // предоставить доступ
            try
            {
                return _count;
            }
            finally
            {
                _lock.ExitReadLock();  // закрыть доступ
            }
        }

        public static void AddToCount(int value)
        {
            _lock.EnterWriteLock();
            _count += value;
            _lock.ExitWriteLock();
        }
    }
}