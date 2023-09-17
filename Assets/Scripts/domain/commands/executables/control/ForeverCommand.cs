using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace domain.commands.executables.control
{
    public class ForeverCommand : ExecutableCommand
    {
        private ExecutableProvider executableProvider;
        
        public ForeverCommand(ExecutableContext context, ExecutableProvider executableProvider) : base(context)
        {
            this.executableProvider = executableProvider;
        }

        public override async UniTask OnEnterAsync()
        {
            if (executableProvider?.Command == null)
                return;
            
            while (true)
            {
                await executableProvider.Command.Execute().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
            }
        }

        public override async UniTask OnExitAsync() { }
        
        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new ForeverCommand(context, executableProvider.Clone(context));
        }

        public override Connection Connection => Connection.Top;
    }
}