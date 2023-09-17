namespace domain.parameter
{
    public class ConditionalParameter : BaseParameter<bool>
    {
        private ContidionalParameterProvider provider;

        public void SetProvider(ContidionalParameterProvider provider)
        {
            this.provider = provider;
        }

        public override bool GetValue()
        {
            return provider?.Validate() ?? false;
        }
    }

    public interface ContidionalParameterProvider
    {
        bool Validate();
    }
}