using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsTimeWaster.Framework;
using WindowsTimeWaster.Properties;

namespace WindowsTimeWaster
{
    static class Program
    {

        private static CommandManager _commandManager;

        private static Worker _worker;
        private static bool _keepIt;

        private const float _delayInSeconds = 2f;

        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TwConsole.ExitRequest += TwConsole_ExitRequest;
            //
            _worker = new Worker(_delayInSeconds);
            _worker.WorkerStarted += _worker_WorkerStarted;
            _worker.WorkerCompleted += work_Completed;
            _commandManager = new CommandManager(_worker);
            //
            TwConsole.WriteLine("{0} - {1}", Resources.Welcome, VERSION);
            _keepIt = true;
            while (_keepIt)
            {
                DisplayMenu();
                ScanInput(ref _keepIt);
                _worker.DoWork().Wait();
            }
            TwConsole.Read();
        }

        static void TwConsole_ExitRequest(object sender, EventArgs e)
        {
            _keepIt = false;
        }

        static void _worker_WorkerStarted(object sender, EventArgs e)
        {
            
        }

        static void work_Completed(object sender, EventArgs e)
        {
            _commandManager.StopProcess();
            MessageBox.Show("The process has finished successfully.");
        }

        private static void DisplayMenu()
        {
            TwConsole.WriteLine();
            TwConsole.WriteLine("{1} - {0} - {1}", Resources.CommandsOptions, MessagesRepository.HEADER_CHARS);
            TwConsole.WriteLine();
            foreach (var option in _commandManager.GetCommandActions())
            {
                TwConsole.WriteLine("   {0} {1} ", option.Key, option.Value);
            }
            TwConsole.WriteLine();
            TwConsole.Write("\t-> ");
        }

        static int ScanInput(ref bool keepIt)
        {
            var temp = TwConsole.ReadLine();
            
            if (temp == null) return 0;

            var input = temp.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (input.First().Equals(CommandManager.EXIT_KEY))
                {
                    keepIt = !keepIt;
                    TwConsole.WriteLine("'Exit' command registered.");
                    TwConsole.WriteLine("Closing...");
                    return 0;
                }
                _commandManager.Process(input);
            }
            catch (InvalidOperationException)
            {
                //Maybe a Enter in blank.
                return 1;
            }
          
            return 1;
        }

        public const String VERSION = "0.9.1";

    }
}
