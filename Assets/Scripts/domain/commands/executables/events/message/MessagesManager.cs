using System;
using System.Collections.Generic;
using System.Linq;

namespace domain.commands.executables.events.message
{
    public class MessagesManager
    {
        private static MessagesManager instance;

        public static MessagesManager Instance => instance ?? (instance = new MessagesManager());

        public event EventHandler OnMessagesUpdated;

        private HashSet<Message> messages = new HashSet<Message>()
        {
            new Message("message1")
        };

        public void AddMessage(string message)
        {
            messages.Add(new Message(message));
            OnMessagesUpdated?.Invoke(this, null);
        }

        public Message[] GetMessages()
        {
            return messages.ToArray();
        }
    }
}