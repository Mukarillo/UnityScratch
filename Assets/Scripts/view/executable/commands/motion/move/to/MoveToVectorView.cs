using domain.commands.executables;
using domain.commands.executables.motion.move.to;
using UnityEngine;
using view.parameter.variable;

namespace view.executable.commands.motion.move.to
{
    public class MoveToVectorView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        [SerializeField] private VariableSocket xSocket;
        [SerializeField] private VariableSocket ySocket;
        [SerializeField] private VariableSocket zSocket;
        [SerializeField] private VariableSocket durationSocket;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            command = new MoveToVectorCommand(context, xSocket.Parameter, ySocket.Parameter, zSocket.Parameter, durationSocket.Parameter);
        }
    }
}