using domain.parameter;

namespace domain.commands.executables.events.keypressed
{
    public class ValidKeysDropdownParameter : DropdownParameter<ValidKeys>
    {
        public ValidKeysDropdownParameter(DropdownParameterProvider<ValidKeys> provider, int initialSelectedIndex = 0) : base(provider, initialSelectedIndex)
        {
        }
    }
}