namespace Solution
{
    public class Program
    {
        private void MyEventHandler1000(object sender, EventArgs e)
        {
            Console.WriteLine("\nTry with 1000 sleep");
            Console.WriteLine("Enter Handler");
            Thread.Sleep(1000);
            Console.WriteLine("Exit Handler");
        }

        private void MyEventHandler6000(object sender, EventArgs e)
        {
            Console.WriteLine("\nTry with 6000 sleep");
            Console.WriteLine("Enter Handler");
            Thread.Sleep(6000);
            Console.WriteLine("Exit Handler");
        }

        private void Run()
        {
            EventHandler h = new EventHandler(MyEventHandler1000);
            AsyncCaller ac = new AsyncCaller(h);
            if (ac.Invoke(5000, this, EventArgs.Empty))
            {
                Console.WriteLine("Completed successfully");
            }
            else
            {
                Console.WriteLine("Timeout occured");
            }

            h = new EventHandler(MyEventHandler6000);
            ac = new AsyncCaller(h);
            if (ac.Invoke(5000, this, EventArgs.Empty))
            {
                Console.WriteLine("Completed successfully");
            }
            else
            {
                Console.WriteLine("Timeout occured");
            }
        }

        public static void Main()
        {
            new Program().Run();
            Console.WriteLine("STOP");
            
            Console.WriteLine("\nServer app outputs:\n");
            MyServer.StartMyServer();
        }
    }

    public class AsyncCaller
    {
        private EventHandler _handler;
        private Thread _thread;

        public AsyncCaller(EventHandler handler)
        {
            _handler = handler;
        }

        public bool Invoke(int time, object sender, EventArgs e)
        {
            _thread = new Thread(Wait);
            //IAsyncResult asyncResult = _handler?.BeginInvoke(sender, e, Aborter, this);
            _handler?.Invoke(sender, e);
            _thread.Start(time);
            _thread.Join();
            _thread = null;
            return true; //asyncResult.IsCompleted;
        }

        private void Wait(object time)
        {
            Thread.Sleep((int)time);
        }

        private void Aborter(IAsyncResult asyncResult)
        {
            //_thread?.Abort();
            _thread = null;
        }
    }
}