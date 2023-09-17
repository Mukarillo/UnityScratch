using domain.commands.executables;
using domain.commands.operators;
using domain.parameter;
using UnityEngine;

namespace view.parameter.conditional.commands
{
    public class NotOperatorView : ConditionalView
    {
        private ContidionalParameterProvider provider;
        public override ContidionalParameterProvider Provider => provider;

        [SerializeField] private ConditionalSocket parameter;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new NotOperator(parameter.Parameter);
        }
    }
}