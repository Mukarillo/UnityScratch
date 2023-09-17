using domain.commands.executables;
using domain.commands.executables.control;
using UnityEngine;
using view.parameter.conditional;

namespace view.executable.commands.control
{
    public class IfElseCommandView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        [SerializeField] private ConditionalSocket parameter;
        [SerializeField] private ExecutableSocket socketTrue;
        [SerializeField] private ExecutableSocket socketFalse;

        private ExecutableProvider providerTrue;
        private ExecutableProvider providerFalse;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            providerTrue = new ExecutableProvider();
            providerFalse = new ExecutableProvider();
            command = new IfElseCommand(context, parameter.Parameter, providerTrue, providerFalse);
            
            socketTrue.SetOwner(this);
            socketFalse.SetOwner(this);
        }

        private void OnEnable()
        {
            socketTrue.OnSocketUpdate.AddListener(SocketTrueUpdated);
            socketFalse.OnSocketUpdate.AddListener(SocketFalseUpdated);
        }

        private void OnDisable()
        {
            socketTrue.OnSocketUpdate.RemoveListener(SocketTrueUpdated);
            socketFalse.OnSocketUpdate.RemoveListener(SocketFalseUpdated);
        }

        private void SocketTrueUpdated(ExecutableView executable)
        {
            providerTrue.SetCommand(executable?.Command);
        }
        
        private void SocketFalseUpdated(ExecutableView executable)
        {
            providerFalse.SetCommand(executable?.Command);
        }
    }
}