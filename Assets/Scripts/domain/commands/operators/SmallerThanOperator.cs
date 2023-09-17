using domain.parameter;
using domain.parameter.variable;

namespace domain.commands.operators
{
    public class SmallerThanOperator : ContidionalParameterProvider
    {
        private readonly VariableParameter left;
        private readonly VariableParameter right;

        public SmallerThanOperator(VariableParameter left, VariableParameter right)
        {
            this.left = left;
            this.right = right;
        }
        
        public bool Validate()
        {
            return left.GetValue() < right.GetValue();
        }
    }
}