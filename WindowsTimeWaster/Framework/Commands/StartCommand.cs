﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WindowsTimeWaster.Framework.Commands
{
    class StartCommand : ITwCommand
    {

        private readonly MessagesRepository _messagesRepository;

        public const String KEY = "-s";

        private const String Help = "Start *required [-s xx | xx milliseconds indicating speed]";

        private bool _isRunning;

        private const int DefaultSeconds = 400;

        public StartCommand()
        {
            _messagesRepository = new MessagesRepository();
        }

        public string ActionKey { get { return KEY; } }

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
                if (!int.TryParse(argument[pos + 1], out time))
                {
                    TwConsole.WriteError("Argument of the type '-s xx' it's not valid. Using default value.");
                }

                time = DefaultSeconds;
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
            catch (Exception)
            {
                throw new TwArgumentNotValidException("Time Interval not in valid format.");
            }

        }

        public void Stop()
        {
            _isRunning = false;
        }

    }
}
