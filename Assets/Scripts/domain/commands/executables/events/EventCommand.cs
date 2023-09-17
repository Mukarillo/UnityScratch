namespace domain.commands.executables.events
{
    public abstract class EventCommand : ExecutableCommand
    {
        protected EventCommand(ExecutableContext context) : base(context)
        {
        }
    }
}