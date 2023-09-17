namespace domain.commands.executables
{
    public class ExecutableProvider
    {
        public ExecutableCommand Command { get; private set; }

        public void SetCommand(ExecutableCommand command)
        {
            Command = command;
        }

        public ExecutableProvider Clone(ExecutableContext context)
        {
            var provider = new ExecutableProvider();
            provider.SetCommand(Command.Clone(context));
            return provider;
        }
    }
}