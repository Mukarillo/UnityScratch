using domain.commands.operators;
using domain.parameter.variable;
using domain.variables;
using TMPro;
using UnityEngine;

namespace view.parameter.variable
{
    public class CustomVariableView : VariableView
    {
        private VariableParameterProvider<float> provider;
        public override VariableParameterProvider<float> Provider => variable.Provider;
        
        [SerializeField] private TextMeshProUGUI text;

        private Variable variable;

        public void SetAsVariable(Variable variable)
        {
            this.variable = variable;
            text.text = variable.Key;
        }

        protected override void OnObjectCloned(BlockView fromBlockView)
        {
            SetAsVariable(((CustomVariableView)fromBlockView).variable);
        }
    }
}