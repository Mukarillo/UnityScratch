using domain.parameter.variable;

namespace domain.commands.operators
{
    public class DivideOperator : VariableParameterProvider<float>
    {
        private readonly VariableParameter left;
        private readonly VariableParameter right;

        public DivideOperator(VariableParameter left, VariableParameter right)
        {
            this.left = left;
            this.right = right;
        }

        public float GetValue()
        {
            var result = left.GetValue() / right.GetValue();
            if (float.IsNaN(result))
                result = 0;
            
            return result;
        }
    }
}