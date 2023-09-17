using domain.commands.executables;
using domain.commands.operators;
using domain.parameter.variable;
using UnityEngine;
using UnityEngine.Serialization;

namespace view.parameter.variable.commands
{
    public class RoundOperatorView : VariableView
    {
        private VariableParameterProvider<float> provider;
        public override VariableParameterProvider<float> Provider => provider;

        [SerializeField] private VariableSocket parameter;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new RoundOperator(parameter.Parameter);
        }
    }
}