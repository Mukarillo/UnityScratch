using domain.parameter;

namespace domain.commands.operators
{
    public class OrOperator : ContidionalParameterProvider
    {
        private readonly ConditionalParameter left;
        private readonly ConditionalParameter right;

        public OrOperator(ConditionalParameter left, ConditionalParameter right)
        {
            this.left = left;
            this.right = right;
        }
        
        public bool Validate()
        {
            return left.GetValue() || right.GetValue();
        }
    }
}