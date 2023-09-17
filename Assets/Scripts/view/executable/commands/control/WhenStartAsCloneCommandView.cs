using domain.commands.executables;
using domain.commands.executables.control;
using UnityEngine;

namespace view.executable.commands.control
{
    public class WhenStartAsCloneCommandView : ExecutableView
    {
        private ExecutableCommand command;
        public override ExecutableCommand Command => command;
        
        public override void Setup(RectTransform root, ExecutableContext context)
        {
            base.Setup(root, context);

            command = new WhenStartAsCloneCommand(context);
        }
    }
}