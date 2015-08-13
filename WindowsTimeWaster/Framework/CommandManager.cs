using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WindowsTimeWaster.Framework.Commands;

namespace WindowsTimeWaster.Framework
{
    class CommandManager
    {

        private readonly Worker _worker;

        // 

        public const String EXIT_KEY = "-n";

        public CommandManager(Worker worker)
        {
            _worker = worker;
            _commands = new Collection<ITwCommand> { 
                new StartCommand(),
                new TimeCommand(_worker),
                new UserInterfaceCommand()
            };
            /*
            _commands.Add("-f", null);
            _commands.Add("-a", null);*/
        }

        public IEnumerable<KeyValuePair<string, string>> GetCommandActions()
        {
            return _commands.Select(command => command.ActionShortcut);
        }

        public void Process(string[] args)
        {
            if (!args.Contains(StartCommand.KEY))
            {
                TwConsole.WriteError("Principal action wasn't found in the arguments.");
                return;
            }

            foreach (var argument in args)
            {
                var cmd = _commands.FirstOrDefault(c => c.ActionKey.Equals(argument));
                if (cmd != null)
                    cmd.Exec(args);
            }
        }

        public void StopProcess()
        {
            foreach (var cmd in _commands)
            {
                cmd.Stop();
            }
        }

        private readonly ICollection<ITwCommand> _commands;

   }
}
