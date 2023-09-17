namespace domain.commands.executables.events.message
{
    public class MessageEventArgs
    {
        public Message message { get; private set; }

        public MessageEventArgs(Message message)
        {
            this.message = message;
        }
    }
}