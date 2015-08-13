using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsTimeWaster.UI;

namespace WindowsTimeWaster.Framework.Commands
{
    class UserInterfaceCommand : ITwCommand
    {

        public const String KEY = "-u";

        private const String Help = "Shows an User Interface";

        private readonly FrmWaiter _form;

        public UserInterfaceCommand()
        {
            _form = new FrmWaiter();
        }

        public string ActionKey { get { return KEY; } }

        public string Description { get; private set; }

        public KeyValuePair<string, string> ActionShortcut
        {
            get {return new KeyValuePair<string, string>(ActionKey, Help);}
        }
        
        public void Exec(object parameter)
        {
            //For now, it's not necessary a parameter
            /*
            if (parameter == null)
                throw new ArgumentNullException("parameter");
            if (!(parameter is Form))
                throw new ArgumentException("", "parameter");
            */
            Task.Run(() => Application.Run(_form));
        }

        public void Stop()
        {
            Application.Exit();
        }
     
    }
}
