using domain.commands.executables;
using domain.commands.executables.control;
using domain.parameter;
using UnityEngine;
using view.parameter.conditional;

namespace view.executable.commands.control
{
    public class IfCommandView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        [SerializeField] private ConditionalSocket parameter;
        [SerializeField] private ExecutableSocket socket;

        private ExecutableProvider provider;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new ExecutableProvider();
            command = new IfCommand(context, parameter.Parameter, provider);
            
            socket.SetOwner(this);
        }

        private void OnEnable()
        {
            socket.OnSocketUpdate.AddListener(SocketUpdated);
        }

        private void OnDisable()
        {
            socket.OnSocketUpdate.RemoveListener(SocketUpdated);
        }

        private void SocketUpdated(ExecutableView executable)
        {
            provider.SetCommand(executable?.Command);
        }
    }
}