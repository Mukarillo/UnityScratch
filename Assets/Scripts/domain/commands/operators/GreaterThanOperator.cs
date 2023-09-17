using domain.parameter;
using domain.parameter.variable;

namespace domain.commands.operators
{
    public class GreaterThanOperator : ContidionalParameterProvider
    {
        private readonly VariableParameter left;
        private readonly VariableParameter right;

        public GreaterThanOperator(VariableParameter left, VariableParameter right)
        {
            this.left = left;
            this.right = right;
        }
        
        public bool Validate()
        {
            return left.GetValue() > right.GetValue();
        }
    }
}