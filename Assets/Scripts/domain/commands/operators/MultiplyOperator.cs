using domain.parameter.variable;

namespace domain.commands.operators
{
    public class MultiplyOperator : VariableParameterProvider<float>
    {
        private readonly VariableParameter left;
        private readonly VariableParameter right;

        public MultiplyOperator(VariableParameter left, VariableParameter right)
        {
            this.left = left;
            this.right = right;
        }

        public float GetValue()
        {
            return left.GetValue() * right.GetValue();
        }
    }
}