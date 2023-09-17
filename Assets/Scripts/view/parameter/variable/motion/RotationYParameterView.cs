using domain.commands.executables;
using domain.parameter.variable;
using domain.parameter.variable.motion;
using UnityEngine;
using view.parameter.variable;

namespace view.parameter.motion
{
    public class RotationYParameterView : VariableView
    {
        private VariableParameterProvider<float> provider;
        public override VariableParameterProvider<float> Provider => provider;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new RotationYParameterProvider(context);
        }
    }
}