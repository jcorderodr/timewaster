using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsTimeWaster.Framework
{
    class TwConsole
    {

        public static event EventHandler ExitRequest;

        static TwConsole()
        {
            Console.Title = String.Format("Time Modder {0}", Program.VERSION);
            Console.CancelKeyPress += Console_CancelKeyPress;
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (ExitRequest != null)
                ExitRequest(sender, e);
        }

        public static int Read()
        {
            return Console.Read();
        }

        public static String ReadLine()
        {
            return Console.ReadLine();
        }

        public static void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public static void WriteLine()
        {
            Console.WriteLine(Environment.NewLine);
        }  
        
        public static void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void WriteError(string message)
        {
            Console.Beep();
            Console.WriteLine(message);
            WriteLine();
        }

    }
}
