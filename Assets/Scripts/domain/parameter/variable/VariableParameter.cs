namespace domain.parameter.variable
{
    public class DefaultVariableParameterProvider<T> : VariableParameterProvider<T>
    {
        private T value;

        public DefaultVariableParameterProvider(T value)
        {
            this.value = value;
        }

        public T GetValue()
        {
            return value;
        }

        public void SetValue(T value)
        {
            this.value = value;
        }
    }
    public class VariableParameter : BaseParameter<float>
    {
        public VariableParameterProvider<float> Provider { get; private set; }
        
        public override float GetValue()
        {
            return Provider.GetValue();
        }
        
        public void SetProvider(VariableParameterProvider<float> provider)
        {
            Provider = provider;
        }
    }

    public interface VariableParameterProvider<out T>
    {
        T GetValue();
    }
}