using domain.parameter;
using domain.variables;

namespace domain.commands.executables.variables
{
    public class VariablesDropdownParameterProvider : DropdownParameterProvider<Variable>
    {
        public Variable[] GetDropdownElements()
        {
            return VariablesManager.Instance.GetAllVariables();
        }
    }
}