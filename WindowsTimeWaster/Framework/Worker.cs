using System;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsTimeWaster.Framework
{
    class Worker
    {

        public event EventHandler<EventArgs> WorkerStarted;
        public event EventHandler<EventArgs> WorkerCompleted;

        public Worker(float seconds)
        {
            _timer = new Timer { AutoReset = false };
            _timer.Elapsed += _timer_Elapsed;
            _seconds = _timer.Interval = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
        }

        public async Task DoWork()
        {
            DoWork(_seconds);
        }

        public async Task DoWork(double seconds)
        {
            _timer.Interval = seconds == System.Threading.Timeout.Infinite ? 
                TimeSpan.FromHours(1).TotalMilliseconds : TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            _timer.Start();
            OnWorkerHandler();
            Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void SetDuration(TimeSpan duration)
        {
            _seconds = duration.TotalSeconds;
        }

        protected virtual void OnWorkerHandler()
        {
            EventHandler<EventArgs> handler = WorkerStarted;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //  after task is called and will not be called, then is completed.
            if (WorkerCompleted != null && !_timer.AutoReset)
            {
                WorkerCompleted(this, EventArgs.Empty);
            }
        }

        private readonly Timer _timer;

        private double _seconds;

    }
}
