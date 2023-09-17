using domain.parameter;

namespace domain.commands.operators
{
    public class NotOperator : ContidionalParameterProvider
    {
        private readonly ConditionalParameter parameter;

        public NotOperator(ConditionalParameter parameter)
        {
            this.parameter = parameter;
        }
        
        public bool Validate()
        {
            return !parameter.GetValue();
        }
    }
}