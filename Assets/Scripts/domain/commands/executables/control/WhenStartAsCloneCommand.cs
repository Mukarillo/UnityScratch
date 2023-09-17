using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace domain.commands.executables.control
{
    public class WhenStartAsCloneCommand : ExecutableCommand
    {
        public WhenStartAsCloneCommand(ExecutableContext context) : base(context) { }

        public override Connection Connection => Connection.Bottom;

        public override async UniTask OnEnterAsync()
        {
            if(ExecutableContext.IsClone)
                await ExecuteNextCommand().AttachExternalCancellation(ExecutableContext.CancellationToken.Token);
        }

        public override async UniTask OnExitAsync() { }

        protected override ExecutableCommand InternalClone(ExecutableContext context)
        {
            return new WhenStartAsCloneCommand(context);
        }
    }
}