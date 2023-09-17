using Cysharp.Threading.Tasks;
using domain.parameter;

namespace domain.commands.executables.control
{
    public class IfCommand : ExecutableCommand
    {
        private readonly ConditionalParameter parameter;
        private readonly ExecutableProvider executableProvider;

        public IfCommand(ExecutableContext context, ConditionalParameter parameter, ExecutableProvider executableProvider) :
            base(context)
        {
            this.parameter = parameter;
            this.executableProvider = executableProvider;
        }

        public override async UniTask OnEnterAsync()
        {
            if (parameter.GetValue() && executableProvider?.Command != null)
                await executableProvider.Command.Execute()
                    .AttachExternalCancellation(ExecutableContext.CancellationToken.Token);

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync()
        {
        }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new IfCommand(context, parameter, executableProvider.Clone(context));
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;
    }
}