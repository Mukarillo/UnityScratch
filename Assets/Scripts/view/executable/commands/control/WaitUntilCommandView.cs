using domain.commands.executables;
using domain.commands.executables.control;
using UnityEngine;
using view.parameter.conditional;
using view.parameter.variable;

namespace view.executable.commands.control
{
    public class WaitUntilCommandView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        [SerializeField] private ConditionalSocket conditional;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            command = new WaitUntilCommand(context, conditional.Parameter);
        }
    }
}