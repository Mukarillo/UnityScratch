using domain.parameter.variable;

namespace domain.commands.operators
{
    public class RoundOperator : VariableParameterProvider<float>
    {
        private readonly VariableParameter parameter;

        public RoundOperator(VariableParameter parameter)
        {
            this.parameter = parameter;
        }

        public float GetValue()
        {
            return (int)parameter.GetValue();
        }
    }
}