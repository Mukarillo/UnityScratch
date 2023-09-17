using domain.commands.executables;
using domain.commands.operators;
using domain.parameter;
using UnityEngine;
using view.parameter.variable;

namespace view.parameter.conditional.commands
{
    public class GreaterThanOperatorView : ConditionalView
    {
        private ContidionalParameterProvider provider;
        public override ContidionalParameterProvider Provider => provider;

        [SerializeField] private VariableSocket left;
        [SerializeField] private VariableSocket right;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new GreaterThanOperator(left.Parameter, right.Parameter);
        }
    }
}