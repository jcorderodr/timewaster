using System;
using System.Collections.Generic;
using System.Threading;

namespace WindowsTimeWaster.Framework.Commands
{
    class TimeCommand : ITwCommand
    {

        private readonly Worker _worker;

        public TimeCommand(Worker worker)
        {
            _worker = worker;
        }

        public string ActionKey { get { return "-t"; } }

        public KeyValuePair<string, string> ActionShortcut
        {
            get
            {
                return new KeyValuePair<string, string>(ActionKey, Description);
            }
        }

        public string Description { get { return String.Format("Time Interval [-t xx | xx seconds greater than 0. {0} is Infinite]", Timeout.Infinite); } }

        public void Exec(object parameter = null)
        {
            if (parameter == null)
                throw new ArgumentNullException();
            var argument = new List<String>(parameter as IEnumerable<String>);
            var pos = argument.IndexOf(ActionKey);
            if(pos < 0)
                throw new TwArgumentNotValidException("Time command not indicated.");
            var time = 0;
            try
            {
                if (int.TryParse(argument[pos + 1], out time))
                {
                    var timeSpan = time != Timeout.Infinite ? TimeSpan.FromSeconds(time) : Timeout.InfiniteTimeSpan;
                    _worker.SetDuration(timeSpan);
                }
                else
                {
                    //TODO: Should'nt do anything.
                }
            }
            catch (Exception)
            {
                throw new TwArgumentNotValidException("Time Interval not in valid format.");
            }
        }

        public void Register(Action action)
        {
            
        }

        public void Stop()
        {
            //Not requiered
        }
    }
}
