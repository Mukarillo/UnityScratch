using domain.commands.executables;
using domain.commands.executables.motion.rotate.by;
using UnityEngine;
using view.parameter.variable;

namespace view.executable.commands.motion.rotate.by
{
    public class RotateByVectorView : ExecutableView
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

            command = new RotateByVectorCommand(context, xSocket.Parameter, ySocket.Parameter, zSocket.Parameter, durationSocket.Parameter);
        }
    }
}