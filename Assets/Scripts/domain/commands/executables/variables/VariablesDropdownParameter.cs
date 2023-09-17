using domain.parameter;
using domain.variables;

namespace domain.commands.executables.variables
{
    public class VariablesDropdownParameter : DropdownParameter<Variable>
    {
        public VariablesDropdownParameter(DropdownParameterProvider<Variable> provider, int initialSelectedIndex = 0) : base(provider, initialSelectedIndex)
        {
        }
    }
}