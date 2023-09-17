using System;
using domain.parameter.variable;

namespace domain.commands.operators
{
    public class RandomBetweenOperator : VariableParameterProvider<float>
    {
        private readonly VariableParameter min;
        private readonly VariableParameter max;

        public RandomBetweenOperator(VariableParameter min, VariableParameter max)
        {
            this.min = min;
            this.max = max;
        }

        public float GetValue()
        {
            var r = new Random();
            return r.Next((int)min.GetValue(), (int)max.GetValue());
        }
    }
}