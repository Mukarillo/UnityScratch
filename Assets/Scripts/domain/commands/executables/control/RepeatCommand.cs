using Cysharp.Threading.Tasks;
using domain.parameter.variable;

namespace domain.commands.executables.control
{
    public class RepeatCommand : ExecutableCommand
    {
        private VariableParameter parameter;
        private ExecutableProvider executableProvider;
        
        public RepeatCommand(ExecutableContext context, VariableParameter parameter, ExecutableProvider executableProvider) : base(context)
        {
            this.parameter = parameter;
            this.executableProvider = executableProvider;
        }

        public override async UniTask OnEnterAsync()
        {
            if (executableProvider?.Command != null)
            {
                var loops = parameter.GetValue();
                for (var i = 0; i < loops; i++)
                {
                    await executableProvider.Command.Execute()
                        .AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
                }
            }
            
            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }
        
        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new RepeatCommand(context, parameter, executableProvider.Clone(context));
        }

        public override Connection Connection => Connection.Top | Connection.Bottom;
    }
}