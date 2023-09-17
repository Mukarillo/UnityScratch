using Cysharp.Threading.Tasks;
using domain.parameter;

namespace domain.commands.executables.control
{
    public class IfElseCommand : ExecutableCommand
    {
        public override Connection Connection => Connection.Top | Connection.Bottom;
        
        private readonly ConditionalParameter parameter;
        private readonly ExecutableProvider providerIfTrue;
        private readonly ExecutableProvider providerIfFalse;

        public IfElseCommand(ExecutableContext context, ConditionalParameter parameter, ExecutableProvider providerIfTrue,
            ExecutableProvider providerIfFalse) : base(context)
        {
            this.parameter = parameter;
            this.providerIfTrue = providerIfTrue;
            this.providerIfFalse = providerIfFalse;
        }

        public override async UniTask OnEnterAsync()
        {
            if (parameter.GetValue() && providerIfTrue?.Command != null)
                await providerIfTrue.Command.Execute().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
            else if (providerIfFalse?.Command != null)
                await providerIfFalse.Command.Execute().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);

            await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync()
        {
        }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new IfElseCommand(context, parameter, providerIfTrue.Clone(context), providerIfFalse.Clone(context));
        }
    }
}