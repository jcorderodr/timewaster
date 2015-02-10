using System;
using System.Collections.Generic;

namespace WindowsTimeWaster.Framework.Commands
{
    public interface ITwCommand
    {
        String ActionKey { get; }

        /// <summary>
        /// Gets the action's key and his description.
        /// </summary>
        KeyValuePair<string, string> ActionShortcut { get; }

        /// <summary>
        /// Description about the ITwCommand funcionality.
        /// </summary>
        String Description { get; }

        /// <summary>
        /// Execute the registered action.
        /// </summary>
        /// <param name="parameter"></param>
        void Exec(object parameter = null);

        void Stop();

    }
}
