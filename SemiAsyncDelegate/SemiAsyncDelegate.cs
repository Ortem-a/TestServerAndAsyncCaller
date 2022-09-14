using System.Threading.Tasks;
using System;

namespace SemiAsyncDelegate
{
    public class AsyncCaller
    {
        private EventHandler _handler;

        public AsyncCaller(EventHandler handler)
        {
            _handler = handler;
        }

        public bool Invoke(int milliseconds, object sender, EventArgs e)
        {
            Task task = Task.Factory.StartNew(() => _handler.Invoke(sender, e));

            return task.Wait(milliseconds);
        }

        public static void Main() { }
    }
}