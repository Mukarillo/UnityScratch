using domain.commands.executables;
using domain.commands.operators;
using domain.parameter;
using UnityEngine;

namespace view.parameter.conditional.commands
{
    public class AndOperatorView : ConditionalView
    {
        private ContidionalParameterProvider provider;
        public override ContidionalParameterProvider Provider => provider;

        [SerializeField] private ConditionalSocket left;
        [SerializeField] private ConditionalSocket right;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new AndOperator(left.Parameter, right.Parameter);
        }
    }
}