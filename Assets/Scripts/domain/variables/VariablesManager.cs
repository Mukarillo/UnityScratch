using System;
using System.Collections.Generic;
using System.Linq;

namespace domain.variables
{
    public class VariablesManager
    {
        private static VariablesManager instance;
        public static VariablesManager Instance => instance ?? (instance = new VariablesManager());

        public event EventHandler OnVariablesUpdated; 

        private readonly Dictionary<string, Variable> repository = new Dictionary<string, Variable>()
        {
            { "my variable", new Variable("my variable", 0) }
        };

        public Variable AddVariable(string key, int defaultValue = 0)
        {
            if (repository.ContainsKey(key))
                return null;
            
            var variable = new Variable(key, defaultValue);
            repository.Add(key, variable);
            
            OnVariablesUpdated?.Invoke(this, null);
            return variable;
        }

        public void RemoveVariable(string key)
        {
            if (key.Equals("my variable"))
                return;
            
            repository.Remove(key);
            
            OnVariablesUpdated?.Invoke(this, null);
        }

        public Variable GetVariableByKey(string key)
        {
            return repository[key];
        }

        public Variable[] GetAllVariables()
        {
            return repository.Select(x => x.Value).ToArray();
        }
    }
}