using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsTimeWaster.UI;

namespace WindowsTimeWaster.Framework.Commands
{
    class StartCommand : ITwCommand
    {

        private readonly MessagesRepository _messagesRepository;

        public const String Key = "-s";

        private const String Help = "Start *required [-s xx | xx milliseconds indicating speed.]";

        private Action _action;

        private bool _isRunning;

        public StartCommand()
        {
            _messagesRepository = new MessagesRepository(); 
        }

        public string ActionKey { get { return Key; } }

        public KeyValuePair<string, string> ActionShortcut
        {
            get
            {
                return new KeyValuePair<string, string>(ActionKey, Help);
            }
        }

        public string Description { get; private set; }

        public void Exec(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException();
            var argument = new List<String>(parameter as IEnumerable<String>);
            var pos = argument.IndexOf(ActionKey);
            if (pos < 0)
                throw new TwArgumentNotValidException("Time command not indicated.");
            try
            {
                var time = 0;
                if (int.TryParse(argument[pos + 1], out time))
                {
                    _isRunning = true;
                    Task.Run(() =>
                    {
                        while (_isRunning)
                        {
                            TwConsole.WriteLine(_messagesRepository.ShowMessages());
                            Task.Delay(TimeSpan.FromMilliseconds(time)).Wait();
                        }
                    }); 
                }
                else
                {
                    TwConsole.WriteError("Argument of the type '-s xx' it's not valid.");
                }
            }
            catch (Exception)
            {
                throw new TwArgumentNotValidException("Time Interval not in valid format.");
            }
            
        }

        public void Register(Action action)
        {
            _action = action;
        }

        public void Stop()
        {
            _isRunning = false;
        }

    }
}
