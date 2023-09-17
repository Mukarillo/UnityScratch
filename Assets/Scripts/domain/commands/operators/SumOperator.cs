using domain.parameter.variable;

namespace domain.commands.operators
{
    public class SumOperator : VariableParameterProvider<float>
    {
        private readonly VariableParameter left;
        private readonly VariableParameter right;

        public SumOperator(VariableParameter left, VariableParameter right)
        {
            this.left = left;
            this.right = right;
        }

        public float GetValue()
        {
            return left.GetValue() + right.GetValue();
        }
    }
}