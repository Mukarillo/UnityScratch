using domain.commands.executables;
using domain.commands.executables.control;
using UnityEngine;
using view.parameter.variable;

namespace view.executable.commands.control
{
    public class WaitSecondsCommandView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        [SerializeField] private VariableSocket variable;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            command = new WaitSecondsCommand(context, variable.Parameter);
        }
    }
}