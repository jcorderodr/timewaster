using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsTimeWaster.Properties;

namespace WindowsTimeWaster.Framework
{
    class MessagesRepository
    {

        public const String HEADER_CHARS = "################";

        private readonly IEnumerable<String> _messages;

        private readonly Random random;

        public MessagesRepository()
        {
            //TODO: Use multiple resources
            _messages = Resources.Messages.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            random = new Random();
        }

        public String ShowMessages()
        {
            return String.Format("{0}... | {1}", _messages.ElementAt(random.Next(0, _messages.Count())), DateTimeOffset.Now);
        }

    }

    public enum MessageType
    {
        RandomProcess,
        Junk
    }

}
