using System;
using domain.parameter.variable;

namespace domain.variables
{
    public class Variable
    {
        public string Key { get; }
        
        public DefaultVariableParameterProvider<float> Provider { get; private set; }

        public event EventHandler ValueChanged;

        public Variable(string key, float value)
        {
            Key = key;
            Provider = new DefaultVariableParameterProvider<float>(value);
        }

        public void ChangeValue(float newValue)
        {
            Provider.SetValue(newValue);
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}