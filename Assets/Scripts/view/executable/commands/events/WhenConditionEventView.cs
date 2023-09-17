using domain.commands.executables;
using domain.commands.executables.events.whencondition;
using UnityEngine;
using view.parameter.conditional;

namespace view.executable.commands.events
{
    public class WhenConditionEventView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        [SerializeField] private ConditionalSocket conditionalSocket;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);
            
            command = new WhenConditionEventCommand(context, conditionalSocket.Parameter);
        }
    }
}