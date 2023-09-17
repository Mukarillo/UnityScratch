using domain.commands.executables;
using domain.commands.operators;
using domain.parameter.variable;
using UnityEngine;

namespace view.parameter.variable.commands
{
    public class RandomBetweenOperatorView : VariableView
    {
        private VariableParameterProvider<float> provider;
        public override VariableParameterProvider<float> Provider => provider;

        [SerializeField] private VariableSocket left;
        [SerializeField] private VariableSocket right;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new RandomBetweenOperator(left.Parameter, right.Parameter);
        }
    }
}