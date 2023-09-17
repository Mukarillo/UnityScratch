using domain.commands.executables;
using domain.commands.executables.events.click;
using UnityEngine;

namespace view.executable.commands.events
{
    public class ClickEventView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;

        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);
            
            command = new ClickEventCommand(context);
        }
    }
}