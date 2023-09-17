using domain.parameter;

namespace domain.commands.executables.events.message
{
    public class MessagesDropdownParameterProvider : DropdownParameterProvider<Message>
    {
        public Message[] GetDropdownElements()
        {
            return MessagesManager.Instance.GetMessages();
        }
    }
}