using domain.parameter;

namespace domain.commands.executables.events.message
{
    public class MessagesDropdownParameter : DropdownParameter<Message>
    {
        public MessagesDropdownParameter(DropdownParameterProvider<Message> provider, int initialSelectedIndex = 0) : base(provider, initialSelectedIndex)
        {
        }
    }
}