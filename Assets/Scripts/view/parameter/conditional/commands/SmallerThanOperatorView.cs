using domain.commands.executables;
using domain.commands.operators;
using domain.parameter;
using UnityEngine;
using view.parameter.variable;

namespace view.parameter.conditional.commands
{
    public class SmallerThanOperatorView : ConditionalView
    {
        private ContidionalParameterProvider provider;
        public override ContidionalParameterProvider Provider => provider;

        [SerializeField] private VariableSocket left;
        [SerializeField] private VariableSocket right;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new SmallerThanOperator(left.Parameter, right.Parameter);
        }
    }
}