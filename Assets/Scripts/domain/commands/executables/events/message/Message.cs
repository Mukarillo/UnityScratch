namespace domain.commands.executables.events.message
{
    public class Message
    {
        public string Value { get; }

        public Message(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}