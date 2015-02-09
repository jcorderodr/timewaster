using System;

namespace WindowsTimeWaster.Framework.Commands
{
    /// <summary>
    /// Represents when an argument for a ITwCommand is not valid.
    /// </summary>
    class TwArgumentNotValidException : Exception
    {
        public TwArgumentNotValidException(string message)
            : base(message)
        {

        }
    }
}
