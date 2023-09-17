using domain.commands.executables;
using domain.commands.executables.control;
using UnityEngine;
using view.parameter.variable;

namespace view.executable.commands.control
{
    public class RepeatCommandView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        [SerializeField] private ExecutableSocket socket;
        [SerializeField] private VariableSocket variable;

        private ExecutableProvider provider;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            provider = new ExecutableProvider();
            command = new RepeatCommand(context, variable.Parameter, provider);
            
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